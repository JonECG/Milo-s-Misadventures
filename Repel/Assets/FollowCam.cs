using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public float tweenCoefficient = 1;
	public GameObject target;
	public Vector3 cameraOffset;
	public Vector3 targetOffset;
	
	private Vector3 lastTo;
	
	// Use this for initialization
	void Start () {
		lastTo = target.transform.position + targetOffset;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = ( this.transform.position * tweenCoefficient + target.transform.position + targetOffset + cameraOffset ) / (tweenCoefficient + 1 );
		lastTo = ( lastTo * tweenCoefficient + target.transform.position + targetOffset ) / ( tweenCoefficient + 1 );
		this.transform.rotation = Quaternion.LookRotation( lastTo-transform.position );
	}
}
