using UnityEngine;
using System.Collections;
using System;

public class Touch
{
	public float x;
	public float y;
	
	public Touch( float x, float y )
	{
		this.x = x;
		this.y = y;
	}
}
public class Swipe : Touch
{
	public float beginX;
	public float beginY;
	public float endX;
	public float endY;
	public float direction;
	
	public Swipe( float x1, float x2, float y1, float y2 ) : base( 0, 0 )
	{
		x = (x1 + x2) / 2;
		y = (y1 + y2) / 2;
		beginX = x1;
		beginY = y1;
		endX = x2;
		endY = y2;
		direction = Mathf.Atan2( y2-y1, x2-x1 );
	}
}

public class TapAndSlash : MonoBehaviour 
{
	public float minSwipeDistance = 60;
	
	public bool drawOnScreen = true;
	
	public Texture2D pointIndicator;
	public Texture2D swipeIndicator;
	
	public IList history;
	
	bool listening;
	
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
	
	private GUITexture point, swipe;
	// Use this for initialization
	void Start () {
		history = new ArrayList();
		timeEnd = 0;
		listening = false;
		point = new GUITexture();
		swipe = new GUITexture();
		
		GameObject pointGO = new GameObject();
		point = pointGO.AddComponent<GUITexture>();
		point.texture = pointIndicator;
		point.pixelInset = new Rect( -pointIndicator.width/2, -pointIndicator.height/2, pointIndicator.width, pointIndicator.height );
		
		GameObject swipeGO = new GameObject();
		swipe = swipeGO.AddComponent<GUITexture>();
		swipe.texture = swipeIndicator;
		swipe.pixelInset = new Rect( 0, -swipeIndicator.height/2, swipeIndicator.width, swipeIndicator.height );
	}
	
	float mouseXDown, mouseYDown;
	bool trackingMouse;
	// Update is called once per frame
	void Update () {
	
		point.enabled = drawOnScreen;
		swipe.enabled = drawOnScreen;
		
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
				trackingMouse = false;
				Action<Swipe> swiping = ( listening ) ? nextSwipeAction : baseSwipeAction;
				Action<Touch> tapping = ( listening ) ? nextTapAction : baseTapAction;
				
				listening = false;
				
				if( (Input.mousePosition.x-mouseXDown) * (Input.mousePosition.x-mouseXDown) + (Input.mousePosition.y-mouseYDown) * (Input.mousePosition.y-mouseYDown) > minSwipeDistance*minSwipeDistance )
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
		else
		{
			if( Input.GetMouseButton( 0 ) )
			{
				trackingMouse = true;
				mouseXDown = Input.mousePosition.x;
				mouseYDown = Input.mousePosition.y;
			}
		}
	}
}
