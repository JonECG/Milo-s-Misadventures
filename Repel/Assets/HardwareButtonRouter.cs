using UnityEngine;
using System.Collections;

public class HardwareButtonRouter : MonoBehaviour {

	public string backButtonDestination = "EXIT";
	public string homeButtonDestination = "EXIT";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKey( KeyCode.Escape ) )
		{
			ScreenTransitioner.Instance.TransitionTo( backButtonDestination );
		}
		if( Input.GetKey( KeyCode.Home ) )
		{
			ScreenTransitioner.Instance.TransitionTo( homeButtonDestination );
		}
	}
}
