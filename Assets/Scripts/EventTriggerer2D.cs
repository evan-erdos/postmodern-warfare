using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTriggerer2D : MonoBehaviour {

	bool wasUsed;

	public MessageTrigger trigger;

	Collider2D coll;

	public UnityEvent m_MessageEvent;

	public float delay;

	public bool isSingleUse;

	void Awake() {
		coll = GetComponent<Collider2D>();
		switch (trigger) {
		case MessageTrigger.Collider:
			//Debug.Log ("Collider!!!");
				if (!coll)
					throw new System.Exception("no collider");
				break;
			case MessageTrigger.Default:
			case MessageTrigger.Event:
			case MessageTrigger.Time: break;
		}
	}

	void Start() {
		if (m_MessageEvent==null)
			m_MessageEvent = new UnityEvent();
		m_MessageEvent.AddListener(OnEvent);
	}

	void OnCollisionEnter2D(Collision2D c) {
		Debug.Log ("Collided!");
		if (trigger!=MessageTrigger.Collider) return;
		if (isSingleUse && wasUsed) return;
		m_MessageEvent.Invoke();
	}

	void OnTriggerEnter2D(Collider2D c) {
		Debug.Log ("TRiggered!");
		if (c.tag != "Player")
			return;
		if (trigger!=MessageTrigger.Collider) return;
		if (isSingleUse && wasUsed) return;
		m_MessageEvent.Invoke();
	}

	public void OnEvent() { wasUsed = true; }//print("something happened!"); }
}
	