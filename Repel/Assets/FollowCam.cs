using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public float tweenCoefficient = 1;
	public GameObject target;
	public Vector3 cameraOffset;
	public Vector3 targetOffset;
	
	private Vector3 lastTo;
	
	private bool snap;
	
	// Use this for initialization
	void Start () {
		snap = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if( snap )
		{
			this.transform.position = target.transform.position + targetOffset + cameraOffset;
			this.transform.rotation = Quaternion.LookRotation( lastTo-transform.position );
			lastTo = target.transform.position + targetOffset;
			snap = false;
		}
		Vector3 intendedFromDelta = ( this.transform.position * tweenCoefficient + target.transform.position + targetOffset + cameraOffset ) / (tweenCoefficient + 1 ) - this.transform.position;
		this.transform.position += intendedFromDelta*Time.deltaTime*60;
		Vector3 intendedToDelta = ( lastTo * tweenCoefficient + target.transform.position + targetOffset ) / ( tweenCoefficient + 1 ) - lastTo;
		lastTo += intendedToDelta*Time.deltaTime*60; 
		this.transform.rotation = Quaternion.LookRotation( lastTo-transform.position );
	}
}
