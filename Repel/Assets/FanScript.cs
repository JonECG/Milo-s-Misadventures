using UnityEngine;
using System.Collections;

public class FanScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GameObject.Find( "LevelController" ).GetComponent<LevelController>().fans.Add( this.gameObject );
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
