using UnityEngine;
using ui=UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Narration : MonoBehaviour {

	string initMessageName = "init";

	float alpha = 1f;
	ui::Text text;
	ui::Image image;
	CanvasGroup group;
	public Message message;

	public Sprite frame;
	public Sprite mallowDog;
	public Sprite commander;
	public Sprite chocolate;

	Dictionary<Speaker,Sprite> speakers;

	void Awake() {
		yml.init();
		text = GetComponent<ui::Text>()
			?? GetComponentInChildren<ui::Text>();
		if (!text)
			throw new System.Exception("No Narration Text");
		foreach (var image in GetComponentsInChildren<ui::Image>())
			if (image.name=="Image")
				this.image = image;
		if (!this.image)
			throw new System.Exception("No Narration Image");
		group = GetComponent<CanvasGroup>()
			?? GetComponentInChildren<CanvasGroup>();
		if (!group)
			throw new System.Exception("No Canvas Group");

		speakers = new Dictionary<Speaker,Sprite>() {
			{Speaker.Default,   frame},
			{Speaker.MallowDog, mallowDog},
			{Speaker.Commander, commander},
			{Speaker.Chocolate, chocolate}};

		DisplayMessage(initMessageName);
	}


	void Update() {
		if (Input.GetButtonDown("Submit"))
			DisplayMessage(message.Next);
	}



	IEnumerator DisplayingMessage(Message message) {
		text.text = message.Description.md();
		image.sprite = speakers[message.speaker];
		image.color = new Color(1,1,1,
			(image.sprite==null)?0:1);
		while (alpha<1f) {
			alpha += 0.1f;
			group.alpha = alpha;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(message.Delay);
		if (message.Persist) yield break;
		while (alpha>0f) {
			alpha -= 0.1f;
			group.alpha = alpha;
			yield return new WaitForEndOfFrame();
		}
	}



	public void DisplayMessage() {
		DisplayMessage(message); }

	public void DisplayMessage(string name) {
		if (name==null) return;
		DisplayMessage(yml.messages[name]); }

	public void DisplayMessage(Message message) {
		StartCoroutine(DisplayingMessage(message));
	}
}
