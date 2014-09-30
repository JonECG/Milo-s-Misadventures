using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject player;
	public Vector3 startPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveTowardsPlayer ();
	}

	void moveTowardsPlayer(){
		Vector3 moveDirection = new Vector3(this.transform.position.x - player.transform.position.x, this.transform.position.y - player.transform.position.y, this.transform.position.z - player.transform.position.z);
		this.transform.position = this.transform.position - (moveDirection.normalized * (Time.deltaTime*2));
	}
}
