using UnityEngine;
using ui=UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Narration : MonoBehaviour {

	string initMessageName = "init";

	float alpha = 1f;
	ui::Text text;
	CanvasGroup group;
	public Message message;


	void Awake() {
		text = GetComponent<ui::Text>()
			?? GetComponentInChildren<ui::Text>();
		if (!text)
			throw new System.Exception("No Narration Text");
		group = GetComponent<CanvasGroup>()
			?? GetComponentInChildren<CanvasGroup>();
		if (!group)
			throw new System.Exception("No Canvas Group");
		DisplayMessage(initMessageName);
	}


	IEnumerator DisplayingMessage(Message message) {
		text.text = message.Description.md();
		while (alpha<1f) {
			alpha += 0.1f;
			group.alpha = alpha;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(message.Delay);
		while (alpha>0f) {
			alpha -= 0.1f;
			group.alpha = alpha;
			yield return new WaitForEndOfFrame();
		}
	}



	public void DisplayMessage() {
		DisplayMessage(message); }

	public void DisplayMessage(string name) {
		DisplayMessage(yml.messages[name]); }

	public void DisplayMessage(Message message) {
		StartCoroutine(DisplayingMessage(message));
	}
}
