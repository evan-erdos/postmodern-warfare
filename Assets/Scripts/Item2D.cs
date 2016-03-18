using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Item2D : MonoBehaviour {

	public UnityEvent m_getEvent;

	void Start() {
		if (m_getEvent==null)
			m_getEvent = new UnityEvent();
		m_getEvent.AddListener(Get);
	}


	void OnCollisionEnter2D(Collision2D c) {
		if (c.rigidbody && c.rigidbody.tag=="Player")
			m_getEvent.Invoke();
	}

	void Get() {
		Destroy(gameObject);
	}
}
