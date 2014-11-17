using UnityEngine;
using System.Collections;
using System;

public enum Direction { UP, DOWN, LEFT, RIGHT };

public class Touch
{
	public float x;
	public float y;
	
	public Touch()
	{
		x = 0;
		y = 0;
	}
	
	public Touch( float x, float y )
	{
		this.x = x;
		this.y = y;
	}
	
	public Vector3 calcWorldPoint()
	{
		Plane plane = new Plane( new Vector3( 0,1,0 ), 0 );
		Ray r = Camera.main.ScreenPointToRay( new Vector3( x, y, 0 ) );
		float dist = 100;
		plane.Raycast( r, out dist );
		return r.origin + r.direction*dist;
	}
}
public class Swipe : Touch
{
	public float beginX;
	public float beginY;
	public float endX;
	public float endY;
	public float directionRad;
	public Direction direction;
	
	public Swipe( float x1, float y1, float x2, float y2 )
	{
		x = (x1 + x2) / 2;
		y = (y1 + y2) / 2;
		beginX = x1;
		beginY = y1;
		endX = x2;
		endY = y2;
		directionRad = Mathf.Atan2( y2-y1, x2-x1 );
		if( Mathf.Abs( x2-x1 ) > Mathf.Abs( y2-y1 ) )
			direction = ((x2-x1)>0) ? Direction.RIGHT : Direction.LEFT;
		else
			direction = ((y2-y1)>0) ? Direction.UP : Direction.DOWN;
	}
}

public class TapAndSlash : MonoBehaviour 
{
	public float minSwipeDistance = 60;
	
	public bool drawOnScreen = true;
	
	public IList history;
	
	bool active = true;
	bool listening;
	//float timeTapStart;
	
	Action<Touch> baseTapAction;
	Action<Swipe> baseSwipeAction;
	
	Action<Touch> nextTapAction;
	Action<Swipe> nextSwipeAction;
	Action nextNoAction;
	
	float timeEnd;
	
	public void Subscribe( Action<Touch> tap, Action<Swipe> swipe )
	{
		baseTapAction = tap;
		baseSwipeAction = swipe;
	}
	
	public void Listen( float timeInSeconds, Action<Touch> tapResponse, Action<Swipe> swipeResponse, Action noResponse )
	{
		listening = true;
		timeEnd = Time.time + timeInSeconds;
		nextTapAction = tapResponse;
		nextSwipeAction = swipeResponse;
		nextNoAction = noResponse;
	}
	
	public void disable()
	{
		active = false;
		listening = false;
		trackingMouse = false;
		//timeTapStart = Time.time;
	}
	
	public void enable()
	{
		active = true;
	}
	
	// Use this for initialization
	void Start () {
		history = new ArrayList();
		timeEnd = 0;
		listening = false;
	}
	
	float mouseXDown, mouseYDown;
	bool trackingMouse;
	
	public void InterruptMouseDown()
	{
		if( trackingMouse )
		{
			float dir = (float) Math.Atan2( Input.mousePosition.y - mouseYDown, Input.mousePosition.x-mouseXDown );
			float dist = (float) Math.Sqrt( (Input.mousePosition.x-mouseXDown) * (Input.mousePosition.x-mouseXDown) + (Input.mousePosition.y-mouseYDown) * (Input.mousePosition.y-mouseYDown) );
			
			
			//timeTapStart = Time.time;
			GetComponent<TapAndSlashDraw>().addUntap( Input.mousePosition.x, Input.mousePosition.y );
			GetComponent<TapAndSlashDraw>().addSwipe( mouseXDown, mouseYDown, Input.mousePosition.x, Input.mousePosition.y );
			
			trackingMouse = false;
			
			Action<Swipe> swiping = ( listening ) ? nextSwipeAction : baseSwipeAction;
			Action<Touch> tapping = ( listening ) ? nextTapAction : baseTapAction;
			
			listening = false;
			
			if( dist > minSwipeDistance )
			{
				Swipe s = new Swipe( mouseXDown, mouseYDown, Input.mousePosition.x, Input.mousePosition.y );
				if( swiping != null )
					swiping(s);
				history.Add( s );
			}
			else
			{
				Touch t = new Touch( Input.mousePosition.x, Input.mousePosition.y );
				if( tapping != null )
					tapping(t);
				history.Add( t );
			}
			if( !listening )
				history.Clear();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if( active )
		{
			
			if( listening )
			{
				if( Time.time > timeEnd )
				{
					if( nextNoAction != null )
						nextNoAction();
					listening = false;
					history.Clear();
				}
			}
			
			if( trackingMouse )
			{

				if( !Input.GetMouseButton( 0 ) )
				{
					InterruptMouseDown();
				}
			}
			else
			{
				if( Input.GetMouseButton( 0 ) )
				{
					//timeTapStart = Time.time;
					GetComponent<TapAndSlashDraw>().addTap( Input.mousePosition.x, Input.mousePosition.y );
					trackingMouse = true;
					mouseXDown = Input.mousePosition.x;
					mouseYDown = Input.mousePosition.y;
				}
			}
		}
	}
	
	void OnGUI()
	{
		if( trackingMouse )
		{
			GetComponent<TapAndSlashDraw>().drawSwipe( mouseXDown, mouseYDown, Input.mousePosition.x, Input.mousePosition.y );
		}
	}
}
