using UnityEngine;
using System.Collections;

public class EnergyController : MonoBehaviour {

	public bool tutorial;
	public Direction tutDirection;
	public bool touched = false;
	public float lastDistance = 500;
	public string tutorialMessage = "";
	
	// Use this for initialization
	void Start () {
		GameObject.Find( "LevelController" ).GetComponent<LevelController>().energies.Add( this.gameObject );
		if( tutorial )
			this.renderer.material.color = new Color( 0.5f, 1, 0 );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
