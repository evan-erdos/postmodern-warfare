using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour {

	RandList<Transform> spawnPoints = new RandList<Transform>();
	public GameObject player;


	void Awake() {
		if (!player)
			throw new System.Exception("no player");
		foreach (Transform child in transform)
			spawnPoints.Add(child);
	}

	void Start() {
		var point = spawnPoints.Next();
		Object.Instantiate(player,point.position,Quaternion.identity);
	}
}
