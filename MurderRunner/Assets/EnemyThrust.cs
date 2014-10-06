using UnityEngine;
using System.Collections;

public class EnemyThrust : AttackBase {


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
	private float travelSpeed;

	public override void attack(Vector3 direction, Vector3 pointOfOrigin)
	{
//		attackGameObject.SetActive (true);
		//attackGameObject.GetComponent<MeshFilter>().renderer.enabled = true;




		timeRemaining = attackDuration;
		Material mat = (Resources.Load("New Material", typeof(Material)) as Material);
		//attackGameObject.renderer.material = mat;
		//attackGameObject.GetComponent<MeshRenderer> ().material = mat;

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
		attackGameObject.renderer.enabled = true;
		//attackGameObject.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.red);

		//attackGameObject.transform.position = pointOfOrigin;



		timeRemaining = attackDuration;


	}


	// Use this for initialization
	void Start () {
		Damage = 5.0f;
		Range = 4.5f;
		attackDuration = 0.1f;
		travelSpeed = 40.0f; 

		attackGameObject = new GameObject ();
		attackGameObject.AddComponent<MeshRenderer> ();
		attackGameObject.AddComponent<MeshFilter> ();
		attackGameObject.GetComponent<MeshFilter>().mesh = GameObject.Find( "CubeShuro" ).GetComponent<MeshFilter>().mesh;
		//attackGameObject.AddComponent<MeshFilter> ();
		//GameObject g = (Resources.Load ("GiveMeFilter") as GameObject);
		//MeshFilter workPlease = (g.GetComponent<MeshFilter>()); 
		//attackGameObject.GetComponent<MeshFilter> ().mesh = workPlease.mesh;
		
		attackGameObject.AddComponent<BoxCollider> ();
		//attackGameObject.GetComponent<BoxCollider>().size = new Vector3 (Range, 1, 1);
		Quaternion q = new Quaternion ();

		//attackGameObject.transform.localScale = attackGameObject.GetComponent<BoxCollider>().size ;
		
		attackGameObject.collider.transform.rotation = q;

		attackGameObject.collider.enabled = false;
		attackGameObject.renderer.enabled = false;
		

		attackGameObject.name = "EnemyWeapon";


	}	
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (PointOfO, PointOfO+newDirection);
		Debug.DrawLine( PointOfO+newDirection, PointOfO+newDirection+new Vector3( 0, 4, 0 ) );
	
		if (timeRemaining > 0.0f) {
			timeRemaining -= Time.deltaTime;
						Vector3 direction = Vector3.Normalize(newDirection);
			attackGameObject.collider.transform.position += direction*travelSpeed*Time.deltaTime;
						
				} else {
			attackGameObject.collider.enabled = false;
			attackGameObject.renderer.enabled = false;
			//attackGameObject.GetComponent<MeshFilter>().renderer.enabled = false;

			//attackGameObject.SetActive(false);

				}

	}
}
