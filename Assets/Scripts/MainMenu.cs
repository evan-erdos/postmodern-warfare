using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject menu;

	void Awake() {
		if (!menu)
			throw new System.Exception("no menu");
	}

	void Start() {
		menu.SetActive(false);
	}

	public void LoadLevel(string scene) {
		SceneManager.LoadScene(scene);
	}

	public void Quit() {
		Application.Quit();
	}


	void Update() {
		if (Input.GetButtonDown("Cancel"))
			menu.SetActive(!menu.activeInHierarchy);
	}


}
