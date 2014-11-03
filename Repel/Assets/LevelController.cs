using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public ArrayList energies = new ArrayList();
	public SpikeController spikes;
	public CheckpointController checkPoints;
	public ShatterBoxController shatters;
	public SlopeController slopes;
	public ArrayList fans = new ArrayList();

	void Awake ()
	{
		spikes = GameObject.Find ("SpikeController").GetComponent<SpikeController> ();
		checkPoints = GameObject.Find ("CheckpointController").GetComponent<CheckpointController> ();
		shatters = GameObject.Find ("ShatterBoxController").GetComponent<ShatterBoxController> ();
		slopes = GameObject.Find ("SlopeController").GetComponent<SlopeController> ();
		bool stop = false;
		}
	// Use this for initialization
	void Start () {
		//energies = new ArrayList();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
