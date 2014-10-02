using UnityEngine;
using System.Collections;

public class Swing : AttackBase {

	
	
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
		timeRemaining = attackDuration;
		
		//attackGameObject.GetComponent<BoxCollider>().size = startingScale;
		
		
		attackGameObject.renderer.material = (Resources.Load("BloodMaterial", typeof(Material)) as Material);
		attackGameObject.collider.transform.position = pointOfOrigin;
		newDirection = direction - pointOfOrigin;
		//attackGameObject.collider.transform.rotation = Quaternion.FromToRotation (attackGameObject.collider.transform.forward, newDirection);
		//attackGameObject.collider.transform.position = pointOfOrigin; 
		PointOfO = pointOfOrigin; 
		//Debug.Log ("New Direction X : " + newDirection.x + ", Y: " + newDirection.y + " , Z : " + newDirection.z);
		//Debug.Log ("Point of Origin X : " + PointOfO.x + ", Y: " + PointOfO.y + " , Z : " + PointOfO.z);
		
		attackGameObject.collider.transform.rotation = Quaternion.LookRotation(newDirection) ;
		attackGameObject.collider.transform.position += (Vector3.Normalize (newDirection) * (Range / 2));
		attackGameObject.collider.transform.Rotate (new Vector3 (0.0f, 1.0f, 0.0f), -90);
		attackGameObject.collider.transform.Translate(new Vector3 (0.0f, 0.0f, 5.0f));
		
		//attackGameObject.renderer.transform.rotation = Quaternion.LookRotation(newDirection) ;
		//attackGameObject.renderer.transform.position += (Vector3.Normalize (newDirection) * (Range / 2));
		//attackGameObject.renderer.transform.Rotate (new Vector3 (0.0f, 1.0f, 0.0f), -90);
		//attackGameObject.renderer.transform.Translate(new Vector3 (0.0f, 0.0f, 5.0f));

		

		//attackGameObject.collider.transform.Translate (Vector3.Normalize (newDirection) * (Range / 2));
		
		
		
		attackGameObject.collider.enabled = true;
		attackGameObject.renderer.enabled = true;
		attackGameObject.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.red);
		
		//attackGameObject.transform.position = pointOfOrigin;
		
		
		
		timeRemaining = attackDuration;
		
		
	}
	
	
	
	
	
	private Vector3 startingScale;
	private float slashSpeed ; 
	private float width = 1.5f; 
	// Use this for initialization
	void Start () {
		Damage = 5.0f;
		Range = 10.0f;
		attackDuration = 0.08f;
		travelSpeed = 40.0f; 
		slashSpeed = 90.0f;
		
		attackGameObject = new GameObject ();
		attackGameObject.AddComponent<MeshRenderer> ();
		attackGameObject.AddComponent<MeshFilter> ();
		attackGameObject.GetComponent<MeshFilter>().mesh = GameObject.Find( "CubeShuro" ).GetComponent<MeshFilter>().mesh;
		
		startingScale = new Vector3 (Range, 1, width);
		attackGameObject.renderer.transform.localScale = startingScale;
		
		
		
		attackGameObject.AddComponent<BoxCollider> ();
		//attackGameObject.GetComponent<BoxCollider>().size = startingScale;
		Quaternion q = new Quaternion ();
		attackGameObject.collider.transform.rotation = q;
		
		attackGameObject.collider.enabled = false;
		attackGameObject.renderer.enabled = false;
		attackGameObject.name = "Weapon";
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (PointOfO, PointOfO+newDirection);
		Debug.DrawLine( PointOfO+newDirection, PointOfO+newDirection+new Vector3( 0, 4, 0 ) );
		
		if (timeRemaining > 0.0f) {
			attackGameObject.collider.transform.Translate( new Vector3(0.0f,0.0f,-slashSpeed*Time.deltaTime));

			timeRemaining -= Time.deltaTime;
		} else {
			attackGameObject.collider.enabled = false;
			attackGameObject.renderer.enabled = false;
			//attackGameObject.SetActive(false);
			
		}
		
	}
}
