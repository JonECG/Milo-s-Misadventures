using UnityEngine;
using System.Collections;

public class ShockwaveController : MonoBehaviour {

	ParticleSystem part;
	
	float aliveTime;
	float currentTime;
	bool shouldPlay;
	// Use this for initialization
	void Start () {
		part = transform.FindChild( "ShockwaveParticleSystem" ).gameObject.GetComponent<ParticleSystem>();
		part.Clear();
		part.enableEmission = false;
		transform.localScale = new Vector3( 0, 0, 0 );
		shouldPlay = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( shouldPlay )
		{
			float percent = 0.3f;
			float scale = 6 * (Mathf.Clamp( currentTime / ( aliveTime * percent * 0.5f ) , 0, 1 )) * (1-Mathf.Clamp( ( currentTime - aliveTime*(1-percent) ) / ( aliveTime * percent ) , 0, 1 ));
			transform.localScale = new Vector3( scale, scale, scale );
			currentTime += Time.deltaTime;
			if( currentTime > aliveTime )
			{
				part.enableEmission = false;
				shouldPlay = false;
			}
		}
	}
	
	public void DisplayShockwave( Vector3 direction, float time )
	{
		currentTime = 0;
		aliveTime = time;
		shouldPlay = true;
		transform.rotation = Quaternion.LookRotation( direction );
		part.enableEmission = true;
	}
}
