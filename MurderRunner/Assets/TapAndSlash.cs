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
	
	public Vector3 calcWorldPoint()
	{
		return new Vector3( 0,0,0 );
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
	public float tapEffectDuration = 0.4f;
	public float minSwipeDistance = 60;
	
	public bool drawOnScreen = true;
	public float drawScale = 0.1f;
	
	public Texture2D pointIndicator;
	public Texture2D swipeIndicator;
	
	public IList history;
	
	bool listening;
	float timeTapStart;
	
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
		point.transform.localScale = new Vector3( 0, 0, 1 );
		
		GameObject swipeGO = new GameObject();
		swipe = swipeGO.AddComponent<GUITexture>();
		swipe.texture = swipeIndicator;
		swipe.pixelInset = new Rect( 0, -swipeIndicator.height/2, swipeIndicator.width, swipeIndicator.height );
		swipe.transform.localScale = new Vector3( 0, 0, 1 );
	}
	
	float mouseXDown, mouseYDown;
	bool trackingMouse;
	// Update is called once per frame
	void Update () {
	
		point.enabled = false;
		swipe.enabled = false;
		
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
			float dir = (float) Math.Atan2( Input.mousePosition.y - mouseYDown, Input.mousePosition.x-mouseXDown );
			float dist = (float) Math.Sqrt( (Input.mousePosition.x-mouseXDown) * (Input.mousePosition.x-mouseXDown) + (Input.mousePosition.y-mouseYDown) * (Input.mousePosition.y-mouseYDown) );
			
			swipe.transform.position = new Vector3( mouseXDown/Screen.width, mouseYDown/Screen.height, 0 );
			swipe.pixelInset = new Rect( 0, dist * (((float)swipeIndicator.height) / swipeIndicator.width )/2, dist, dist * (((float)swipeIndicator.height) / swipeIndicator.width ) );
			swipe.transform.rotation = Quaternion.AngleAxis( dir, new Vector3( 0,0,1 ) );
			
			if( !Input.GetMouseButton( 0 ) )
			{
				timeTapStart = Time.time;
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
		else
		{
			if( Input.GetMouseButton( 0 ) )
			{
				timeTapStart = Time.time;
				trackingMouse = true;
				mouseXDown = Input.mousePosition.x;
				mouseYDown = Input.mousePosition.y;
				point.transform.position = new Vector3( mouseXDown/Screen.width, mouseYDown/Screen.height, 0 );
				point.pixelInset = new Rect( -Screen.height * drawScale/2, -Screen.height * drawScale/2, Screen.height * drawScale, Screen.height * drawScale );
			}
		}
	}
	
	void drawSwipe( float x1, float y1, float x2, float y2 )
	{
		Matrix4x4 matrixBackup = GUI.matrix;
		float dir = -Mathf.Atan2( y2 - y1, x2 - x1 )*Mathf.Rad2Deg;
		float dist = Mathf.Sqrt( (x2-x1) * (x2-x1) + (y2-y1) * (y2-y1) );
		
		swipe.transform.position = new Vector3( x1/Screen.width, y1/Screen.height, 0 );
		swipe.pixelInset = new Rect( 0, dist * (((float)swipeIndicator.height) / swipeIndicator.width )/2, dist, dist * (((float)swipeIndicator.height) / swipeIndicator.width ) );
		swipe.transform.rotation = Quaternion.AngleAxis( dir, new Vector3( 0,0,1 ) );
		
		
		GUIUtility.RotateAroundPivot( dir, new Vector2(x1,Screen.height - y1));
		float height = Mathf.Min( dist*1.1f*(((float)swipeIndicator.height)) / swipeIndicator.width, Screen.height*drawScale );
		GUI.DrawTexture(new Rect( x1, Screen.height - y1 - height/2, dist*1.1f, height ), swipeIndicator);
		GUI.matrix = matrixBackup;
	}
	
	void OnGUI()
	{
		float animationProgress = (Time.time - timeTapStart) / tapEffectDuration;
		if( trackingMouse )
		{
			drawSwipe( mouseXDown, mouseYDown, Input.mousePosition.x, Input.mousePosition.y );
			
			if( animationProgress < 1 )
			{
				Color orig = GUI.color;
				GUI.color = new Color( 1.0f,1.0f,1.0f, animationProgress );
				GUI.DrawTexture(new Rect( Input.mousePosition.x - 40*(1-animationProgress), Screen.height - Input.mousePosition.y - 40*(1-animationProgress), 80*(1-animationProgress), 80*(1-animationProgress) ), pointIndicator);
				GUI.color = orig;
			}
		}
		else
		{
			if( animationProgress < 1 )
			{
				drawSwipe( mouseXDown + (Input.mousePosition.x-mouseXDown)*animationProgress, mouseYDown + (Input.mousePosition.y-mouseYDown)*animationProgress, Input.mousePosition.x, Input.mousePosition.y );
				
				Color orig = GUI.color;
				GUI.color = new Color( 1.0f,1.0f,1.0f, 1-animationProgress );
				GUI.DrawTexture(new Rect( Input.mousePosition.x - 40*(2*animationProgress), Screen.height - Input.mousePosition.y - 40*(2*animationProgress), 80*(2*animationProgress), 80*(2*animationProgress) ), pointIndicator);
				GUI.color = orig;
			}
		}
	}
}
