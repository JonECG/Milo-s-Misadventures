using UnityEngine;
using System.Collections;

//Remember to import System to use Action
using System;

public class TestTapSwipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log( "Init!" );
		this.gameObject.AddComponent<Thrust>();
		
		TapAndSlash ts = GetComponent<TapAndSlash>();
		ts.Subscribe
		( 	
			(Touch t1) => 
			{
				Debug.Log( "Woah: " + t1.calcWorldPoint().x + ", " + t1.calcWorldPoint().y + ", " + t1.calcWorldPoint().z );
				Debug.Log( "Initial Tap" );

				this.gameObject.GetComponent<Thrust>().attack (t1.calcWorldPoint(), this.gameObject.transform.position);



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
//
//Vector3 end = new Vector3(s.x,s.y, 1.0f);
//
////Debug.Log("End X: " + end.x + " , Y: " + end.y ); 
// 
//
//
//
//Plane ground = new Plane( new Vector3( 0,1,0), new Vector3( 0,0,0 ) );
//
//Ray r = Camera.main.ScreenPointToRay(end);
//float dist = 31.07353f;
//bool hit = Physics.Raycast(r);
//Debug.Log("HIT? : " + hit);
//Debug.Log("Dist : " + dist); 
//
//Vector3 pos = r.origin + r.direction*dist;
////Vector3 onMap = Camera.main.ScreenToWorldPoint(end);
//
//
//Debug.Log("OnMap X: " + pos.x + " , Y : " + pos.y + " , Z: " + pos.z);


			}
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
