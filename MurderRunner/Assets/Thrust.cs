using UnityEngine;
using System.Collections;

public class Thrust : AttackBase {


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
//		attackGameObject.SetActive (true);
		attackGameObject.renderer.material = (Resources.Load("BloodMaterial", typeof(Material)) as Material);
		attackGameObject.collider.transform.position = pointOfOrigin;
		newDirection = direction - pointOfOrigin;
		//attackGameObject.collider.transform.rotation = Quaternion.FromToRotation (attackGameObject.collider.transform.forward, newDirection);
		//attackGameObject.collider.transform.position = pointOfOrigin; 
		PointOfO = pointOfOrigin; 
		//Debug.Log ("New Direction X : " + newDirection.x + ", Y: " + newDirection.y + " , Z : " + newDirection.z);
		//Debug.Log ("Point of Origin X : " + PointOfO.x + ", Y: " + PointOfO.y + " , Z : " + PointOfO.z);
		
		attackGameObject.collider.transform.rotation = Quaternion.LookRotation(newDirection) ;
		attackGameObject.collider.transform.Rotate (new Vector3 (0.0f, 1.0f, 0.0f), 90);
		attackGameObject.collider.transform.position += (Vector3.Normalize (newDirection) * (Range / 2));
		//attackGameObject.collider.transform.Rotate (new Vector3 (0.0f, 1.0f, 0.0f), -90);
		//attackGameObject.collider.transform.Translate (Vector3.Normalize (newDirection) * (Range / 2));
		


		attackGameObject.collider.enabled = true;
		attackGameObject.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.red);

		//attackGameObject.transform.position = pointOfOrigin;



		timeRemaining = attackDuration;


	}


	// Use this for initialization
	void Start () {
		Damage = 5.0f;
		Range = 4.5f;
		attackDuration = 1.0f;


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
		Debug.DrawLine (PointOfO, PointOfO+newDirection);
		Debug.DrawLine( PointOfO+newDirection, PointOfO+newDirection+new Vector3( 0, 4, 0 ) );
	
		if (attackDuration > 0.0f) {
						attackDuration -= Time.deltaTime;
				} else {
			attackGameObject.collider.enabled = false;
			//attackGameObject.SetActive(false);

				}

	}
}
