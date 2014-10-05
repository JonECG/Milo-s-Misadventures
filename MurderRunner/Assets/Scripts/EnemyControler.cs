using UnityEngine;
using System.Collections;

public class EnemyControler : MonoBehaviour {


	public GameObject enemyToSpawn;
	public float secondsBetweenSpawns = 1.0f;
	public int numberOfSpawns = 1;
	public int health = 10;
	public int enemyLimit = 20;

	private int enemyiesSpawned = 0;
	private float lastSpawn = 0.0f;


	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (lastSpawn <= 0) {
			for(int i = 0; i < numberOfSpawns; i++){
				if(enemyiesSpawned < enemyLimit) {
					Vector3 offset = new Vector3(Random.Range(-5,5),0,Random.Range(-5,5));
					Enemy enemy = (Instantiate (enemyToSpawn, this.transform.position + offset, Quaternion.identity) as GameObject).GetComponent<Enemy>();
					enemy.killed += enemyKilled; 
					lastSpawn = secondsBetweenSpawns;
					enemyiesSpawned++;
				}
			}
		}
		else {
			lastSpawn = lastSpawn - Time.deltaTime;
		}
	}

	public void takeDamage(int damage) {
		health = health - damage;
		if (health <= 0) {
			Destroy(this.gameObject);
		}
	}

	public void enemyKilled() {
		enemyiesSpawned--;
	}
}
