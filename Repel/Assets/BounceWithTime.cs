using UnityEngine;
using System.Collections;

public class BounceWithTime : MonoBehaviour {

	public float bounceTime = 2;
	public float bounceHeight = 2;
	
	private float currentTime;
	private Vector3 startOffset;
	private Vector3 startScale;
	
	// Use this for initialization
	void Start () {
		currentTime = 0;
		startOffset = this.transform.localPosition;
		startScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		currentTime = currentTime % bounceTime;
		
		this.transform.localPosition = startOffset + new Vector3( 0, bounceHeight * Mathf.Max( 0, Mathf.Sin( Mathf.PI * currentTime/bounceTime ) ) );
		this.transform.localScale = new Vector3( this.transform.localScale.x, startScale.y * ( -Mathf.Sin( Mathf.PI * currentTime/bounceTime )*0.5f + 1.5f ), startScale.z * ( Mathf.Sin( Mathf.PI * currentTime/bounceTime )*0.5f + 1 ) );
	}
}
