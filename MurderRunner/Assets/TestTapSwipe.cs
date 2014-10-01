using UnityEngine;
using System.Collections;

//Remember to import System to use Action
using System;

public class TestTapSwipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log( "Init!" );
		
		TapAndSlash ts = GetComponent<TapAndSlash>();
		ts.Subscribe
		( 	
			(Touch t1) => 
			{
				Debug.Log( "Initial Tap" );
				ts.Listen( 1,
					(Touch t2) =>
					{
						Debug.Log( "Tap after Initial Tap" );
					},
					(Swipe s2) =>
					{
						Debug.Log( "Swipe after Initial Tap" );
					},
					() =>
					{
						Debug.Log( "No action after Initial Tap" );
					}
				);
					
			},
			(Swipe s) =>
			{
				Debug.Log( "Initial Swipe" );
			}
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
