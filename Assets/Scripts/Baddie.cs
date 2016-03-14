
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Baddie : MonoBehaviour, IDamageable {

	bool wait;
	bool immune;
	public float range = 8f;
	public float delay = 2f;
	public float force = 2f;

	LayerMask layerMask;

	Collider2D[] results = new Collider2D[10];

	public GameObject deadReplacement;
	public GameObject projectile;
	public GameObject explosion;
	public UnityEvent m_killEvent;

	public void Kill() {
		if (immune) return;
		if (explosion)
			Object.Instantiate(explosion,transform.position,Quaternion.identity);
		GameObject temp;
		if (deadReplacement) {
			temp = Object.Instantiate(
				deadReplacement,
				transform.position,
				Quaternion.identity) as GameObject;
			var inner = temp.GetComponent<Baddie>();
			if (inner) inner.Immune(2f);
			foreach (var rb in temp.GetComponentsInChildren<Rigidbody2D>())
				rb.AddExplosionForce(4000f, transform.position,400f,200);
		} Destroy(gameObject);
	}

	public void Immune(float time) {
		StartCoroutine(Immunity(time)); }

	public IEnumerator Immunity(float time) {
		if (immune) yield break;
		immune = true;
		yield return new WaitForSeconds(time);
		immune = false;
	}

	public void Apply(float damage) {
		if (damage>0) m_killEvent.Invoke(); }

	void Awake() { layerMask = ~LayerMask.NameToLayer("Player"); }


	void Start() {
		if (m_killEvent==null)
			m_killEvent = new UnityEvent();
		m_killEvent.AddListener(Kill);
	}

	IEnumerator Throwing(GameObject target) {
		if (wait) yield break;
		wait = true;
		if (projectile && target) {
			var instance = Object.Instantiate(
				projectile,
				transform.position,
				Quaternion.identity) as GameObject;
			var trajectory = new Vector2(
				target.transform.position.x-transform.position.x,
				target.transform.position.y-transform.position.y);
			instance.GetComponent<Rigidbody2D>().AddForce(
				trajectory*force,ForceMode2D.Impulse);
		}
		yield return new WaitForSeconds(delay);
		wait = false;
	}


	void FixedUpdate() {
		Physics2D.OverlapCircleNonAlloc(
			transform.position, range, results, layerMask);
		foreach (var hit in results)
			if (hit && hit.attachedRigidbody && hit.attachedRigidbody.tag=="Player")
				StartCoroutine(Throwing(
					hit.attachedRigidbody.GetComponent<PlayerMovement>().gameObject));
	}
}

















