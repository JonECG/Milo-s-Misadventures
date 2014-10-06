using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject player;
	public GameObject particleBlood;
	public float refreshTime = 3;
	public GameObject bloodStainEffect;
	private int health = 1;
	
	private float timeTilNextUpdate;
	private Vector3 target;
	public delegate void Killed();
	public event Killed killed;

	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<EnemyThrust>();
		this.player = (GameObject.Find ("CubeShuro") as GameObject);
		target = this.player.transform.position;
		timeTilNextUpdate = refreshTime;
	}
	
	// Update is called once per frame
	void Update () {
		moveTowardsPlayer ();
		if (health <= 0) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider theCollision){
		if (theCollision.gameObject.name=="Weapon") {
			Hit (1);
		}
	}


	void moveTowardsPlayer(){
		timeTilNextUpdate -= Time.deltaTime;
		if( timeTilNextUpdate < 0 )
		{
			timeTilNextUpdate += refreshTime + Random.Range( 0, 1 );
			target = this.player.transform.position;
			
			if( (target - this.transform.position).sqrMagnitude < 16 )
			{
				this.GetComponent<EnemyThrust>().attack(target, this.gameObject.transform.position);
				target = this.transform.position;
			}
		}
		Vector3 moveDirection = (target - transform.position);
		this.transform.position = this.transform.position + (moveDirection.normalized * Mathf.Min( (Time.deltaTime*2), moveDirection.magnitude ) );
	}

	void Hit(int damage){
		Vector3 vectorToPlayer = new Vector3(this.transform.position.x - player.transform.position.x, this.transform.position.y - player.transform.position.y, this.transform.position.z - player.transform.position.z);
		Quaternion bloodRotation = new Quaternion();
		bloodRotation.SetFromToRotation (new Vector3(0,0,1), vectorToPlayer); 
		Vector3 bloodPosition = new Vector3 (this.transform.position.x, this.transform.position.y+0.5f, this.transform.position.z);
		var bloodEffect = (Instantiate (particleBlood, bloodPosition, bloodRotation) as GameObject);
		Destroy (bloodEffect, 1.0f);
		var bloodStain = (Instantiate (bloodStainEffect, this.transform.position, this.transform.rotation) as GameObject);
		Destroy (bloodStain, 1.0f);
		health -= damage;
		KillCounterScript.Increment ();
	}
}
