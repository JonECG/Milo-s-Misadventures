using UnityEngine;
using System.Collections;

//Remember to import System to use Action
using System;

public class TestTapSwipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log( "Init!" );
		this.gameObject.AddComponent<InitialSwipeAttack>();
		
		TapAndSlash ts = GetComponent<TapAndSlash>();
		ts.Subscribe
		( 	
			(Touch t1) => 
			{
				Debug.Log( "Woah: " + t1.calcWorldPoint().x + ", " + t1.calcWorldPoint().y + ", " + t1.calcWorldPoint().z );
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
				Vector2 start = new Vector2(s.beginX,s.beginY);
				Vector2 end = new Vector2(s.endX,s.endY);



				end = ((start-end)/ 2) + end;

				Debug.Log("End X: " + end.x + " , Y: " + end.y ); 
				 
				

				Ray r = Camera.main.ScreenPointToRay(end);


				Vector3 onMap = r.GetPoint(Camera.main.gameObject.transform.position.y);


				this.gameObject.GetComponent<InitialSwipeAttack>().attack (onMap, this.gameObject.transform.position);
			}
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
