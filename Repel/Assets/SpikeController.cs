using UnityEngine;
using System.Collections;

public class SpikeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find( "LevelController" ).GetComponent<LevelController>().spikes.Add( this.gameObject );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
