using UnityEngine;
using ui=UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public PlayerMovement player;
	ui::Scrollbar scrollbar;



	void Start() {
		if (!player)
			throw new System.Exception("No Player");
		scrollbar = GetComponent<ui::Scrollbar>();
		if (!scrollbar)
			throw new System.Exception("No Scrollbar");
	}

	void Update() {
		scrollbar.size = player.Health / player.maxHealth;
	}
}
