using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {
	
	public bool goal = false;
	public bool triggered = false;
	
	// Use this for initialization
	void Start () {
		GameObject.Find( "LevelController" ).GetComponent<LevelController>().checkPoints.add( this.gameObject );
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
