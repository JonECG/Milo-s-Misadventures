using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {



	public ArrayList checkpoints = new ArrayList();
	
	public bool goal = false;
	public bool triggered = false;
	public GameObject player;
	public GameObject checkPointEffect;
	public AudioClip hitCheckpointSound;



	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		
	}

	public void add(GameObject toAdd)
	{
		checkpoints.Add (toAdd);
		}

	
	// Update is called once per frame
	void Update () {

		if (player != null) {
						for (int i = 0; i < checkpoints.Count; i++) {
								float dist = (player.transform.position - ((GameObject)checkpoints [i]).transform.position).sqrMagnitude;
								if (dist < 6) {
										if (((GameObject)checkpoints [i]).GetComponent<CheckPointScript> ().goal) {
											player.GetComponent<PlayerController>().completeLevel();
											LevelNumberHolder.setHighestLevel();
										} else {
												if (!((GameObject)checkpoints [i]).GetComponent<CheckPointScript> ().triggered) {
														((GameObject)checkpoints [i]).GetComponent<CheckPointScript> ().triggered = true;
														if (!player.GetComponent<PlayerController> ().startGame) {
																var partEfIn = (Instantiate (checkPointEffect, ((GameObject)checkpoints [i]).transform.position, Quaternion.identity) as GameObject);
																Destroy (partEfIn, 1.0f);
																audio.PlayOneShot (hitCheckpointSound);
														}
												}
												//((GameObject)checkpoints [i]).renderer.material.color = new Color (0.5f, 1, 0);
												PlayerController.startPosition = new Vector3 (player.transform.position.x, player.transform.position.y, 0);
												PlayerController.hasCheck = true;
										}
								}
						}
				}
	}
}
