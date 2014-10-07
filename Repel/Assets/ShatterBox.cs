using UnityEngine;
using System.Collections;

public class ShatterBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find( "LevelController" ).GetComponent<LevelController>().shatters.Add( this.gameObject );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
