using UnityEngine;
using System.Collections;

public class AlwaysRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.RotateAround (this.gameObject.transform.position, this.gameObject.transform.forward,500.0f*Time.deltaTime);
	}
}
