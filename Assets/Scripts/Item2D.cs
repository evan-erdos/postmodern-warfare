
using UnityEngine;
using UnityEngine.Events;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Item2D : MonoBehaviour {

	public bool isSingleUse;

	public UnityEvent m_TakeEvent;
	public UnityEvent m_DropEvent;

	void Awake() {
		if (m_DropEvent==null)
			m_DropEvent = new UnityEvent();
		m_DropEvent.AddListener(OnDrop);
		if (m_TakeEvent==null)
			m_TakeEvent = new UnityEvent();
		m_TakeEvent.AddListener(OnTake);
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (!c.rigidbody) return;
		var other = c.rigidbody.GetComponent<PlayerMovement>();
		if (other==null) return;
		if (other.tag=="Player") Take();
	}

	void OnTake() {
		if (isSingleUse) Destroy(gameObject);
	}

	void OnDrop() { }


	void Take() { m_TakeEvent.Invoke(); }

	void Drop() { m_TakeEvent.Invoke(); }
}








