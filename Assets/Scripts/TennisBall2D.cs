using UnityEngine;
using System.Collections;

public class TennisBall2D : MonoBehaviour {

	public GameObject explosion;

	public float Damage { get { return 20f; } }


	void Start() {

	}

	void OnCollisionEnter2D(Collision2D c) {
		if (!c.rigidbody || c.rigidbody.tag!="Player") return;
		if (!c.rigidbody.GetComponent<PlayerMovement>()) return;
		c.rigidbody.GetComponent<PlayerMovement>().Apply(Damage);
		Object.Instantiate(explosion,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
