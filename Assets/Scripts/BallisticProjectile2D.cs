using UnityEngine;
using System.Collections;

public class BallisticProjectile2D : MonoBehaviour {

	bool wait = true;
	public float delay = 3f;

	public GameObject explosion;

	public float Damage { get { return 50f; } }

	void Start() {
		StartCoroutine(TriggerDelay(delay));
	}

	IEnumerator TriggerDelay(float delay) {
		wait = true;
		yield return new WaitForSeconds(delay);
		wait = false;
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (wait) return;
		if (c.rigidbody) {
			var player = c.rigidbody.GetComponent<PlayerMovement>();
			if (player) player.Apply(Damage);
		}
		var temp = Object.Instantiate(explosion,transform.position,Quaternion.identity) as GameObject;
		foreach (var rb in temp.GetComponentsInChildren<Rigidbody2D>())
				rb.AddExplosionForce(4000f, transform.position,400f,200);
		Destroy(gameObject);
	}
}
