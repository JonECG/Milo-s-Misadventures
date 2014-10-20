using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

	public string toLoad;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			RaycastHit hit = new RaycastHit ();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 200)) {
				resolveCollision (hit);
			}
		}
	}

	private void resolveCollision(RaycastHit hit)
	{
		if (hit.collider==this.gameObject.collider) {
			Application.LoadLevel(toLoad);
		}
		
	}



}
