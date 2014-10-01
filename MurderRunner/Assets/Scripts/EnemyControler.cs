using UnityEngine;
using System.Collections;

public class EnemyControler : MonoBehaviour {


	public GameObject enemyToSpawn;
	public float secondsBetweenSpawns = 1.0f;
	private float lastSpawn = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lastSpawn <= 0) {
			var enemy = (Instantiate (enemyToSpawn, this.transform.position, Quaternion.identity) as GameObject);
			lastSpawn = secondsBetweenSpawns;
		}
		else {
			lastSpawn = lastSpawn - Time.deltaTime;
		}
	}

}
