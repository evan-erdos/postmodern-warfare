using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpecialLevel1StoryEvent : MonoBehaviour {

	public UnityEvent m_StoryEvent;

	public GameObject narration;
	public GameObject player;
	public GameObject baddie;
	public GameObject handgun;
	public GameObject flyTarget;

	public float gunForce = 60000f;
	public float flySpeed = 600f;
	public float flyDelay = 10f;

	Vector3 speed;

	public int currMsgIdx = 0;

	public bool storyEventHappening = false;
	public bool flyingAway = false;

	public static string[] msgNames = {
		"carl_1",
		"carl_2",
		"narrator_5",
		"carl_3",
		"carl_4",
		"carl_5",
		"carl_6",
		"carl_7",
		"carl_8",
		"carl_9",
		"carl_10",
		"carl_11",
		"narrator_6",
		"carl_12",
		"carl_13",
		"carl_14",
		"carl_15",
		"carl_16",
		"carl_18",
		"carl_19",
		"carl_20",
		"carl_21",
		"carl_22",
		"carl_23",
		"carl_24",
		"narrator_7",
		"narrator_8",
		"narrator_9",
		"narrator_10",
		"narrator_11",
		"narrator_12",
		"narrator_13",
		"narrator_14",
	};



	// Use this for initialization
	void Awake () {
		if (m_StoryEvent==null)
			m_StoryEvent = new UnityEvent();
		m_StoryEvent.AddListener(OnStory);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (flyingAway) {
			baddie.transform.localPosition = Vector3.SmoothDamp (
				baddie.transform.localPosition,
				flyTarget.transform.position, ref speed, 0.8f,
				flyDelay, Time.deltaTime);
			if (Vector3.SqrMagnitude(baddie.transform.localPosition - 
				flyTarget.transform.position) < 5) 
			//if (baddie.transform.localPosition == flyTarget.transform.position)
				flyingAway = false;
			return;
		}

	
		
		if (!storyEventHappening)
			return;
		if (Input.GetButtonDown ("Throw")) {
			if (currMsgIdx == 4) {
				GameObject gun = (GameObject)Instantiate (handgun,
					                 player.transform.position, Quaternion.identity);
				gun.transform.GetComponent<Rigidbody2D> ().AddForce (transform.right * gunForce);
			} else if (currMsgIdx == 25) {
				flyingAway = true;
			} else {
				narration.GetComponent<Narration> ().DisplayMessage (msgNames [currMsgIdx]);
			}
			currMsgIdx++;
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
