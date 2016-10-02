using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpecialLevelBossStoryEvent : MonoBehaviour {

	public UnityEvent m_StoryEvent;

	public GameObject narration;
	public GameObject handgun;

	public float gunForce = 60000f;
	public float flySpeed = 600f;
	public float flyDelay = 10f;

	Vector3 speed;

	public int currMsgIdx = 0;

	public bool storyEventHappening = false;
	public bool flyingAway = false;

	public static string[] msgNames = {
		"boss_0",
		"boss_1",
		"boss_2",
		"boss_3"};


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
			if (currMsgIdx == 14) {
				flyingAway = true;
			} else if (currMsgIdx >= 14 && flyingAway == false) {
				storyEventHappening = false;
			} else {
				narration.GetComponent<Narration> ().DisplayMessage (msgNames [currMsgIdx]);
			}
			currMsgIdx++;
		}

	}

	void OnTriggerEnter2D(Collider2D c) {

		Debug.Log ("Trigggggerrrrrrr");

		if (c.tag != "Player") return;

		transform.GetComponent<BoxCollider2D> ().enabled = false;

		storyEventHappening = true;

		//Debug.Log ("1");

		// Stop Player from moving
		c.GetComponent<PlayerMovement>().enabled = false;
		//c.GetComponent<SpecialStoryEvent>().enabled = true;

		//Debug.Log ("2");

		// Start story narration event
		Story();
		//Debug.Log ("3");

	}


	void OnStory() {}

	void Story() { m_StoryEvent.Invoke(); }
}
