using UnityEngine;
using System.Collections;

public class SimpleDoor : MonoBehaviour {

	bool isOpen = true;
	// Use this for initialization
	void Start () {
	
	}

	void ToggleOpen() {
		if (isOpen) {
			
			isOpen = false;
		
		}
	}
	
	// Update is called once per frame
	void Update () {
			return;
	}
}
