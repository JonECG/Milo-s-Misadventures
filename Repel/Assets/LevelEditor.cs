using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

	public GameObject indicator;
	
	private int numClicks = 0;
	private Vector3[] clickHistory;
	private GameObject[] clickReps;
	
	// Use this for initialization
	void Start () {
		clickHistory = new Vector3[10];
		clickReps = new GameObject[10];
		for( int i = 0; i < clickReps.Length; i++ )
		{
			clickReps[i] = Instantiate( indicator ) as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for( int i = 0; i < clickHistory.Length; i++ )
		{
			clickReps[i].transform.position = ( i < numClicks ) ? clickHistory[i] : new Vector3( -100000,-100000,-1000000 );
		}
		
		Vector3 vec = new Vector3( ( Input.GetKey(KeyCode.D) ? 1 : 0 ) - ( Input.GetKey(KeyCode.A) ? 1 : 0 ),
		                          ( Input.GetKey(KeyCode.W) ? 1 : 0 ) - ( Input.GetKey(KeyCode.S) ? 1 : 0 ),
		                          ( Input.GetKey(KeyCode.E) ? 1 : 0 ) - ( Input.GetKey(KeyCode.Q) ? 1 : 0 ) );
		vec *= 4 * Time.deltaTime * ( -transform.position.z/10 );
		transform.position += vec;
		
		Plane plane = new Plane( new Vector3( 0, 0, 1 ), new Vector3( 0, 0, 0 ) );
		float dist = 0;
		Ray r = camera.ScreenPointToRay( Input.mousePosition );
		if ( plane.Raycast( r, out dist ) )
		{
			Vector3 inter = r.origin + r.direction * dist;
			
			if( Input.GetKey( KeyCode.LeftControl ) )
			{
				inter.x = Mathf.Round( inter.x );
				inter.y = Mathf.Round( inter.y );
			}
			
			indicator.transform.position = inter;
		}
		
		if( Input.GetMouseButtonDown( 1 ) )
		{
			numClicks = 0;
		}
		
		if( Input.GetMouseButtonDown( 0 ) )
		{
			clickHistory[ numClicks++ ] = indicator.transform.position;
			if( numClicks >= 3 )
			{
				GameObject obj = ((GameObject)Instantiate( Resources.Load( "Slope", typeof( GameObject ) ), this.transform.position, Quaternion.Euler( 0,0,0 ) ));
				SlopeScript ss = obj.GetComponent<SlopeScript>();
				
				int lx = 0;
				int rx = 0;
				int ty = 0;
				int sty = 0;
				int by = 0;
				
				for( int i = 1; i < 3; i++ )
				{
					if( clickHistory[i].x < clickHistory[lx].x ) lx = i;
					if( clickHistory[i].x > clickHistory[rx].x ) rx = i;
					if( clickHistory[i].y < clickHistory[by].y ) by = i;
					if( clickHistory[i].y > clickHistory[ty].y ) ty = i;
				}
				
				for( int i = 1; i < 3; i++ )
				{
					if( clickHistory[i].y < clickHistory[ty].y && clickHistory[i].y > clickHistory[by].y )
						sty = i;
				}
				
				obj.transform.position = new Vector3( (clickHistory[lx].x+clickHistory[rx].x)/2, (clickHistory[by].y+clickHistory[ty].y)/2, 0 );
				obj.transform.localScale = new Vector3( clickHistory[rx].x - clickHistory[lx].x, clickHistory[ty].y - clickHistory[by].y, 5 );
				
				float ly = ( clickHistory[ty].x < clickHistory[sty].x ) ? clickHistory[ty].y : clickHistory[sty].y ;
				float ry = ( clickHistory[ty].x < clickHistory[sty].x ) ? clickHistory[sty].y : clickHistory[ty].y ;
				ss.leftPerc = ( ly - clickHistory[by].y ) / ( clickHistory[ty].y - clickHistory[by].y );
				ss.rightPerc = ( ry - clickHistory[by].y ) / ( clickHistory[ty].y - clickHistory[by].y );
				
				numClicks = 0;
			}
		}
	}
}
