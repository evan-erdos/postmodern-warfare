using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StoryEvent : MonoBehaviour {

	public UnityEvent m_StoryEvent;

	// Use this for initialization
	void Awake () {
		if (m_StoryEvent==null)
			m_StoryEvent = new UnityEvent();
		m_StoryEvent.AddListener(OnStory);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c) {

		Debug.Log ("Trigggggerrrrrrr");

		if (c.tag != "Player") return;

		// Stop Player from moving
		c.GetComponent<PlayerMovement>().enabled = false;

		// Start story narration event
		Story();

	}

//	void OnCollisionEnter(Collision2D c) {
//
//		Debug.Log ("Trigggggerrrrrrr");
//
//		if (c.transform.tag != "Player") return;
//
//		// Stop Player from moving
//		c.transform.GetComponent<PlayerMovement>().enabled = false;
//
//		// Start story narration event
//		Story();
//
//	}

	void OnStory() {}

	void Story() { m_StoryEvent.Invoke(); }
}
