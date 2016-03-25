using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Hoop : MonoBehaviour {

	public UnityEvent m_HoopEvent;
	public GameObject explosion;

	void Start() {
		if (m_HoopEvent==null)
			m_HoopEvent = new UnityEvent();
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (!c.attachedRigidbody
		|| c.attachedRigidbody.tag!="key") return;
		m_HoopEvent.Invoke();
		Object.Instantiate(
			explosion,
			transform.position,
			Quaternion.identity);
		Destroy(c.attachedRigidbody.gameObject);
	}
}
