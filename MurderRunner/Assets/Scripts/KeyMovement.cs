using UnityEngine;
using System.Collections;

public class KeyMovement : MonoBehaviour {

	public float speed = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 newVelocity = this.gameObject.rigidbody.velocity;
		if(Input.GetKeyDown(KeyCode.D))
		{
			newVelocity.x+=speed;
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			newVelocity.x-=speed;
		}


		this.gameObject.rigidbody.velocity = newVelocity;
	}
}
