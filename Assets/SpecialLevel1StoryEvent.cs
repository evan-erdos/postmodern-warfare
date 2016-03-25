using UnityEngine;
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
		"carl_11"
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
