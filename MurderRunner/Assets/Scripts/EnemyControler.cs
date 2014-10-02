using UnityEngine;
using System.Collections;

public class EnemyControler : MonoBehaviour {


	public GameObject enemyToSpawn;
	public float secondsBetweenSpawns = 1.0f;
	public int numberOfSpawns = 1;
	private float lastSpawn = 0.0f;


	// Use this for initialization
	void Start () {
	
	}

	public static void dealDamage(Vector3 impact)
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lastSpawn <= 0) {
			for(int i = 0; i < numberOfSpawns; i++){
				Vector3 offset = new Vector3(Random.Range(-5,5),0,Random.Range(-5,5));
				var enemy = (Instantiate (enemyToSpawn, this.transform.position + offset, Quaternion.identity) as GameObject);
				lastSpawn = secondsBetweenSpawns;
			}
		}
		else {
			lastSpawn = lastSpawn - Time.deltaTime;
		}
	}

}
