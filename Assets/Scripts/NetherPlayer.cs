using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NetherPlayer : MonoBehaviour {

	void Start() {
		StartCoroutine(Killing(10));
	}

	IEnumerator Killing(float delay) {
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}


