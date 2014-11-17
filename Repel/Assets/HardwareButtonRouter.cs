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
			if( backButtonDestination == "EXIT" )
				Application.Quit();
			else
				Application.LoadLevel( backButtonDestination );
		}
		if( Input.GetKey( KeyCode.Home ) )
		{
			if( homeButtonDestination == "EXIT" )
				Application.Quit();
			else
				Application.LoadLevel( homeButtonDestination );
		}
	}
}
