
using UnityEngine;
using UnityEngine.Events;
using ui=UnityEngine.UI;
using System.Collections;

public class Finale : MonoBehaviour {

	public GameObject explosion;

	ui::Image image;
	AudioSource audioSource;

	public UnityEvent m_QuitEvent;


	void Start() {
		if (m_QuitEvent==null)
			m_QuitEvent = new UnityEvent();
		m_QuitEvent.AddListener(OnQuit);
		audioSource = GetComponent<AudioSource>();
		image = GetComponent<ui::Image>();
		image.color = new Color(1,1,1,0);
		StartCoroutine(Quitting());
	}

	IEnumerator Quitting() {
		yield return new WaitForSeconds(90);
		m_QuitEvent.Invoke();
	}


	void OnQuit() {
		audioSource.Stop();
		image.color = new Color(1,1,1,1);
		StartCoroutine(AfterQuitting());
	}


	IEnumerator AfterQuitting() {
		yield return new WaitForSeconds(5);
		Object.Instantiate(
			explosion,
			transform.position,
			Quaternion.identity);
		yield return new WaitForSeconds(1);
		Application.Quit();
	}
}
