using UnityEngine;
using System.Collections;

public class TutorialView : MonoBehaviour {

	public Texture2D hand;
	public float animationDuration = 3;
	public float handDistance = 0.1f;
	private float time;
	private bool repeat = false;
	private bool visible;
	private string message = "";
	
	private float shadeAmount = 0;
	private float maxShadeAmount = 0.5f;
	
	private Vector2 start, end;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log( repeat );
		float target = (repeat) ? 1 : 0;
		float tweenDisp = 10;
		shadeAmount = ( shadeAmount * tweenDisp + target ) / ( tweenDisp + 1 );
		
		time += Time.deltaTime;
		if( time > animationDuration )
		{
			if( repeat )
				time = 0;
			else
				visible = false;
		}
	}
	
	public void show( Direction dir, string message = "" )
	{
		switch( dir )
		{
			case Direction.UP:
				show( new Vector2( 0.5f, 0.25f ), new Vector2( 0.5f, 0.75f ), message );
				break;
			case Direction.DOWN:
				show( new Vector2( 0.5f, 0.75f ), new Vector2( 0.5f, 0.25f ), message );
				break;
			case Direction.LEFT:
				show( new Vector2( 0.75f, 0.5f ), new Vector2( 0.25f, 0.5f ), message );
				break;
			case Direction.RIGHT:
				show( new Vector2( 0.25f, 0.5f ), new Vector2( 0.75f, 0.5f ), message );
				break;
		}
	}
	
	public void show( Vector2 start, Vector2 end, string message = "" )
	{
		this.start = start;
		this.end = end;
		
		time = -1;
		repeat = true;
		visible = true;
		this.message = message;
	}
	
	public void unshow()
	{
		repeat = false;
	}
	
	private static Texture2D _staticRectTexture;
	private static GUIStyle _staticRectStyle;
	
	// Note that this function is only meant to be called from OnGUI() functions.
	public static void GUIDrawRect( Rect position, Color color )
	{
		if( _staticRectTexture == null )
		{
			_staticRectTexture = new Texture2D( 1, 1 );
		}
		
		if( _staticRectStyle == null )
		{
			_staticRectStyle = new GUIStyle();
		}
		
		_staticRectTexture.SetPixel( 0, 0, color );
		_staticRectTexture.Apply();
		
		_staticRectStyle.normal.background = _staticRectTexture;
		
		GUI.Box( position, GUIContent.none, _staticRectStyle );		
	}
	
	int lastPhase = 0;
	void OnGUI()
	{
		var centeredStyle = new GUIStyle( GUI.skin.label );
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = 32;
		GUIDrawRect( new Rect(0,0,Screen.width,Screen.height), new Color( 0,0,0, shadeAmount * maxShadeAmount ) );
		
		if( visible )
		{
			GUI.Label ( new Rect (20, 20, 3*Screen.width/4, 200), message, centeredStyle);
			TapAndSlashDraw draw = GetComponent<TapAndSlashDraw>();
			
			float animationProgress = time / animationDuration*2;
			
			if( animationProgress < 0.25f )
			{
				float handX = Screen.width*(start.x + handDistance*(1-animationProgress / 0.25f));
				float handY = Screen.height*(start.y + handDistance*(1-animationProgress / 0.25f));
				
				Color orig = GUI.color;
				GUI.color = new Color( 1.0f,1.0f,1.0f, animationProgress / 0.25f );
				GUI.DrawTexture(new Rect( handX, Screen.height - handY, 80, 80), hand);
				GUI.color = orig;
				
				lastPhase = 0;
			}
			else
			if( animationProgress < 0.75f )
			{
				float handX = Screen.width*(start.x + (end.x-start.x)*((animationProgress-0.25f) / 0.5f));
				float handY = Screen.height*(start.y + (end.y-start.y)*((animationProgress-0.25f) / 0.5f));			
				
				if( lastPhase == 0 )
					draw.addTap( Screen.width*start.x, Screen.height*start.y );
				draw.drawSwipe( Screen.width*start.x, Screen.height*start.y, handX, handY );
				
				GUI.DrawTexture(new Rect( handX, Screen.height - handY, 80, 80), hand);	
				
				lastPhase = 1;
			}
			else
			if( animationProgress < 1 )
			{
				float handX = Screen.width*(end.x + handDistance*((animationProgress-0.75f) / 0.25f));
				float handY = Screen.height*(end.y + handDistance*((animationProgress-0.75f) / 0.25f));
				
				if( lastPhase == 1 )
				{
					draw.addSwipe( Screen.width*start.x, Screen.height*start.y, Screen.width*end.x, Screen.height*end.y );
					draw.addUntap( Screen.width*end.x, Screen.height*end.y );
				}
				
				Color orig = GUI.color;
				GUI.color = new Color( 1.0f,1.0f,1.0f, 1 - ((animationProgress-0.75f) / 0.25f) );
				GUI.DrawTexture(new Rect( handX, Screen.height - handY, 80, 80), hand);
				GUI.color = orig;
				
				lastPhase = 2;
			}
		}
	}
}
