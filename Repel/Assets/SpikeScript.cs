using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {
	GameObject cont; 
	bool done = false;
	// Use this for initialization
	void Start () {
		cont = GameObject.Find ("LevelController");
		LevelController adder = cont.GetComponent<LevelController> ();
		adder.spikes.addSpike (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}