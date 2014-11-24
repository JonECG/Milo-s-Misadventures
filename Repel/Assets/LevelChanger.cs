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
			ScreenTransitioner.Instance.TransitionTo(toLoad);
		}
	}

}
