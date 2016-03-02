using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadSceneOnClick : MonoBehaviour {

	bool wait;

	public void OnLoadScene(string scene) { StartCoroutine(StartGame(scene,1f)); }

	IEnumerator StartGame(string scene, float delay) {
		if (wait) yield break;
		wait = true;
		CameraFade.StartAlphaFade(
            new Color(0,0,0),false,0f,delay,
            ()=> {
                if (!Camera.main) return;
                Camera.main.cullingMask = 0;
                Camera.main.clearFlags =
                    CameraClearFlags.SolidColor;
                Camera.main.backgroundColor =
                    new Color(0,0,0);
                SceneManager.LoadScene(scene);});
		yield return new WaitForSeconds(delay);
	}
}
