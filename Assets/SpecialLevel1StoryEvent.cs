﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpecialLevel1StoryEvent : MonoBehaviour {

	public UnityEvent m_StoryEvent;

	public GameObject narration;
	public GameObject player;
	public GameObject handgun;

	public float gunForce = 60000f;

	public int currMsgIdx = 0;

	public bool storyEventHappening = false;

	public static string[] msgNames = {
		"choco-baddie1",
		"choco-baddie2",
		"choco-baddie3",
		"choco-baddie4",
		"choco-baddie5",
		"choco-baddie6",
		"choco-baddie7",
		"choco-baddie8"
	};



	// Use this for initialization
	void Awake () {
		if (m_StoryEvent==null)
			m_StoryEvent = new UnityEvent();
		m_StoryEvent.AddListener(OnStory);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!storyEventHappening)
			return;
		if (Input.GetButtonDown ("Throw")) {
			if (currMsgIdx == 6) {
				GameObject gun = (GameObject) Instantiate (handgun,
					player.transform.position, Quaternion.identity);
	
				gun.transform.GetComponent<Rigidbody2D> ().AddForce (transform.right * gunForce);
			
			} else {
				currMsgIdx++;
				narration.GetComponent<Narration> ().DisplayMessage (msgNames [currMsgIdx]);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c) {

		Debug.Log ("Trigggggerrrrrrr");

		if (c.tag != "Player") return;

		storyEventHappening = true;

		Debug.Log ("1");

		// Stop Player from moving
		c.GetComponent<PlayerMovement>().enabled = false;
		//c.GetComponent<SpecialStoryEvent>().enabled = true;

		Debug.Log ("2");

		// Start story narration event
		Story();
		Debug.Log ("3");

	}
		

	void OnStory() {}

	void Story() { m_StoryEvent.Invoke(); }
}