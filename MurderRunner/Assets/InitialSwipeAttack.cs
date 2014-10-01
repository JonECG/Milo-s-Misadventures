using UnityEngine;
using System.Collections;

public class InitialSwipeAttack : AttackBase {


	public override float Damage {
		get;
		set;
	}
	
	public override float Range {
		get;
		set;
	}

	public override float timeRemaining {
		get;
		set;
	}

	private GameObject attackGameObject;

	public float attackDuration;
	private Vector3 newDirection;
	private Vector3 PointOfO; 
	public override void attack(Vector3 direction, Vector3 pointOfOrigin)
	{
		//attackGameObject.SetActive (true);
//		attackGameObject.renderer.material = (Resources.Load("BloodMaterial", typeof(Material)) as Material);

		newDirection = new Vector3 (direction.x, 0.0f, direction.z);
		newDirection = newDirection - pointOfOrigin;
		//newDirection = newDirection - pointOfOrigin; 
		//attackGameObject.collider.transform.rotation = Quaternion.FromToRotation (attackGameObject.collider.transform.forward, newDirection);
		//attackGameObject.collider.transform.position = pointOfOrigin; 
		PointOfO = pointOfOrigin; 
		Debug.Log ("New Direction X : " + newDirection.x + ", Y: " + newDirection.y + " , Z : " + newDirection.z);
		Debug.Log ("Point of Origin X : " + PointOfO.x + ", Y: " + PointOfO.y + " , Z : " + PointOfO.z);

		//attackGameObject.collider.transform.Rotate (new Vector3 (0.0f, 1.0f, 0.0f), 90);
		//attackGameObject.collider.enabled = true;
		//attackGameObject.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.red);

		//attackGameObject.transform.position = pointOfOrigin;
		timeRemaining = attackDuration;
	}



	public InitialSwipeAttack()
	{
		Damage = 5.0f;
		Range = 50.0f;
		attackDuration = 1.0f;

	}

	// Use this for initialization
	void Start () {
		attackGameObject = new GameObject ();
		attackGameObject.AddComponent<MeshRenderer> ();
		
		attackGameObject.AddComponent<BoxCollider> ();
		attackGameObject.GetComponent<BoxCollider>().size = new Vector3 (Range, 1, 1);
		Quaternion q = new Quaternion ();
		attackGameObject.collider.transform.rotation = q;

		attackGameObject.collider.enabled = false;


	}	
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (PointOfO, newDirection);
	
		if (attackDuration > 0.0f) {
						attackDuration -= Time.deltaTime;
				} else {
			attackGameObject.collider.enabled = false;
			//attackGameObject.SetActive(false);

				}

	}
}
