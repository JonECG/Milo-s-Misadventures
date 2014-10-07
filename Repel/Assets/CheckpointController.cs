using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find( "LevelController" ).GetComponent<LevelController>().checkpoints.Add( this.gameObject );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
