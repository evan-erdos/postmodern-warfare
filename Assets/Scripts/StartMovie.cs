﻿using UnityEngine;
using ui=UnityEngine.UI;
using System.Collections;

public class StartMovie : MonoBehaviour {
    void Start() {
    	((MovieTexture) GetComponent<ui::Image>().material.mainTexture).Play();
    	StartCoroutine(DestroyMovie(12));
    }


    IEnumerator DestroyMovie(float delay) {
    	yield return new WaitForSeconds(delay);
    	Destroy(gameObject);
    }
}
