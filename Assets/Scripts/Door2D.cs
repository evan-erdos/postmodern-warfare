using UnityEngine;
using System.Collections;

public class Door2D : MonoBehaviour {


	public float delay = 0.1f;

	GameObject door;

	Vector3 init, open, target, speed;

	public bool IsOpen {
		get { return isOpen; }
		set {
			isOpen = value;
			target = (isOpen)?open:init;
		}
	} public bool isOpen;

	void Awake() {
		foreach(Transform child in transform)
			if (child.name=="target")
				open = child.localPosition;
			else if (child.name=="door") {
				door = child.gameObject;
				init = door.transform.localPosition;
			}
	}

	void FixedUpdate() {
        door.transform.localPosition = Vector3.SmoothDamp(
            door.transform.localPosition,
            target, ref speed, 0.8f,
            delay, Time.deltaTime);
	}


	public void Toggle() { IsOpen = !IsOpen; }
	public void Open() { IsOpen = true; }
	public void Shut() { IsOpen = false; }


}
