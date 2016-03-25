using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

	bool wait;
	float reloadDelay = 3f;
	public float speed;
	public float speedForce = 50f;
	public float maxSpeed = 10f;
	public float jumpForce = 1500f;
	public float maxHealth = 350f;
	public float stretchAmount = .9f;
	public float squishAmount = -0.65f;
	Rigidbody2D _rigidbody2D;
	Animator anim;
	public GameObject potentialGun;
	public LayerMask layerMask;
	public GameObject gun;
	public GameObject corpse;
	public AudioClip stretchSound;
	public AudioClip squishSound;


	public float Health {
		get { return health; }
		private set {
			health = value;
			if (health<0) Kill();
		}
	} float health;


	public bool HasGun {get;set;}
	public bool IsGrounded {get;set;}
	public bool IsJumping {get;set;}
	public bool IsSquishing {get;set;}
	public bool IsStretching {get;set;}

	public bool IsFacingForward {
		get { return isFacingForward; }
		set { isFacingForward = value;
			transform.localScale = (isFacingForward)
				? new Vector3(1,1,1) : new Vector3(-1,1,1);
		}
	} bool isFacingForward;

	public float Radius { get { return 2f; } }


	public void Apply(float damage) { Health -= damage; }


	void Awake() {
		layerMask = ~LayerMask.NameToLayer("Gun");
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}

	void Start() {
		health = maxHealth;
	}


	IEnumerator PlayingSound(AudioClip clip,float volume) {
		if (wait) yield break;
		wait = true;
		GetComponent<AudioSource>().PlayOneShot(clip, volume);
		yield return new WaitForSeconds(clip.length);
		wait = false;
	}

	IEnumerator Killing() {
		yield return new WaitForSeconds(reloadDelay);
		print("asdf");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	public void Kill() {
		if (!corpse) return;
		var replacement = Object.Instantiate(
			corpse,
			transform.position,
			transform.rotation) as GameObject;
		foreach (var rb in replacement.GetComponentsInChildren<Rigidbody2D>())
			rb.velocity = _rigidbody2D.velocity;
		Destroy(gameObject);
		StartCoroutine(Killing());
	}


	void Update() {
		var wasSquishing = IsSquishing;
		var wasStretching = IsStretching;

		IsSquishing = Input.GetButton("Squish");
		IsStretching = Input.GetButton("Stretch") && !IsSquishing;

		if (IsSquishing && !wasSquishing)
			StartCoroutine(PlayingSound(squishSound,0.8f));
		else if (IsStretching && !wasStretching)
			StartCoroutine(PlayingSound(stretchSound,0.8f));

		IsSquishing = Input.GetButton("Squish");
		IsStretching = Input.GetButton("Stretch") && !IsSquishing;

		speed = Input.GetAxis("Horizontal")*0.5f;

		if (Input.GetButtonDown("Next"))
			SceneManager.LoadScene(
				SceneManager.GetActiveScene().buildIndex+1);
		else if (Input.GetButtonDown("Prev"))
			SceneManager.LoadScene(
				SceneManager.GetActiveScene().buildIndex-1);
	}


	void LateUpdate() {
		anim.SetBool("Grounded", IsGrounded);
		anim.SetBool("Squishing", IsSquishing);
		anim.SetBool("Stretching", IsStretching);
		anim.SetFloat("Speed", Mathf.Abs(speed));
		/* Get Player to face correct direction */
		if (Input.GetAxis("Horizontal")>0.1f)
			IsFacingForward = true;
		else if (Input.GetAxis("Horizontal")<-0.1f)
			IsFacingForward = false;
		if (Input.GetButtonDown("Jump") && IsGrounded)
			IsJumping = true;
		if (Input.GetButtonDown("Throw")) {
			if (HasGun) ThrowGun();
			else GetGun();
		}
	}



	void OnTriggerEnter2D(Collider2D c) {
		if (!c.GetComponent<Rigidbody2D>()
		|| c.GetComponent<Rigidbody2D>().tag!="Gun") return;
		if (!HasGun) potentialGun = c.GetComponent<Rigidbody2D>().gameObject;
	}

	public virtual void ThrowGun() {
		gun.GetComponent<Gun2D>().IsHeld = false;
		//var pos = gun.transform.position;
		gun.transform.parent = null;
		//gun.transform.position = pos;
		HasGun = false;
		potentialGun = null;
		var force = gun.GetComponent<Gun2D>().force;
		gun.GetComponent<Rigidbody2D>().AddForce(
			((IsFacingForward)?1:-1)*transform.right*force);
		gun.GetComponent<Rigidbody2D>().AddTorque(600f);
	}


	void GetGun() {
		if (potentialGun && Mathf.Abs((potentialGun.transform.position-transform.position).sqrMagnitude)<8f)
			GetGun(potentialGun);
	}

	public virtual void GetGun(GameObject gun) {
		gun.transform.parent = transform;
		//gun.transform.localPosition = Vector3.zero;
		HasGun = true;
        gun.GetComponent<Gun2D>().IsHeld = true;
        this.gun = gun;
	}

	void FixedUpdate() {
		GetComponent<BoxCollider2D>().size = new Vector2(
			(IsStretching)?(stretchAmount):(2f), (IsSquishing)?(0.5f):(2f));
		GetComponent<BoxCollider2D>().offset = new Vector2(
			0, (IsSquishing)?(squishAmount):(0.1f));
		float h = Input.GetAxis("Horizontal");
		_rigidbody2D.AddForce((Vector2.right*speedForce)*h);
		if (_rigidbody2D.velocity.x > maxSpeed)
			_rigidbody2D.velocity = new Vector2(maxSpeed, _rigidbody2D.velocity.y);
		else if (_rigidbody2D.velocity.x<-maxSpeed)
			_rigidbody2D.velocity = new Vector2(-maxSpeed, _rigidbody2D.velocity.y);
		if (IsJumping) {
			_rigidbody2D.AddForce(Vector2.up * jumpForce);
			IsJumping = false;
		} if (gun && gun.transform.parent==transform)
			gun.transform.localPosition = Vector3.zero;
	}
}
