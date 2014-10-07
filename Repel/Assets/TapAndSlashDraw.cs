using UnityEngine;
using System.Collections;

public class TapAndSlashDraw : MonoBehaviour {

	public float tapEffectDuration = 0.4f;
	public float drawScale = 0.1f;
	
	public Texture2D pointIndicator;
	public Texture2D swipeIndicator;
	
	public ArrayList reps;
	
	public abstract class Representation
	{
		public float timeAlive;
		public abstract void draw( TapAndSlashDraw context );
	}
	
	public class TapRep : Representation
	{
		float x, y;
		
		public TapRep( float x, float y )
		{
			this.x = x;
			this.y = y;
		}
		
		public override void draw( TapAndSlashDraw context )
		{
			float animationProgress = timeAlive / context.tapEffectDuration;
			
			Color orig = GUI.color;
			GUI.color = new Color( 1.0f,1.0f,1.0f, animationProgress );
			GUI.DrawTexture(new Rect( x - 40*(1-animationProgress), Screen.height - y - 40*(1-animationProgress), 80*(1-animationProgress), 80*(1-animationProgress) ), context.pointIndicator);
			GUI.color = orig;
		}
	}
	
	public class UntapRep : Representation
	{
		float x, y;
		
		public UntapRep( float x, float y )
		{
			this.x = x;
			this.y = y;
		}
		
		public override void draw( TapAndSlashDraw context  )
		{
			float animationProgress = timeAlive / context.tapEffectDuration;
			
			Color orig = GUI.color;
			GUI.color = new Color( 1.0f,1.0f,1.0f, 1-animationProgress );
			GUI.DrawTexture(new Rect( x - 40*(2*animationProgress), Screen.height - y - 40*(2*animationProgress), 80*(2*animationProgress), 80*(2*animationProgress) ), context.pointIndicator);
			GUI.color = orig;
		}
	}

	public class SwipeRep : Representation
	{
		float x1, y1, x2, y2;
		
		public SwipeRep( float x1, float y1, float x2, float y2 )
		{
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
		}
		
		public override void draw( TapAndSlashDraw context  )
		{
			float animationProgress = timeAlive / context.tapEffectDuration;
			
			float mx = (x2-x1)*animationProgress + x1;
			float my = (y2-y1)*animationProgress + y1;
			
			context.drawSwipe( mx, my, x2, y2 );
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		reps = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
	{
		for( int i = 0; i < reps.Count; i++ )
		{
			Representation rep = (Representation)reps[i];
			if( rep.timeAlive > tapEffectDuration )
			{
				reps.RemoveAt( i );
				i--;
			}
			else
			{
				rep.timeAlive += Time.deltaTime;
			}
		}
	}
	
	void OnGUI()
	{
		for( int i = 0; i < reps.Count; i++ )
		{
			Representation rep = (Representation)reps[i];
			rep.draw( this );
		}
	}
	
	public void addTap( float x, float y )
	{
		reps.Add( new TapRep( x, y ) );
	}
	
	public void addUntap( float x, float y )
	{
		reps.Add( new UntapRep( x, y ) );
	}
	
	public void addSwipe( float x1, float y1, float x2, float y2 )
	{
		reps.Add( new SwipeRep( x1, y1, x2, y2 ) );
	}
	
	public void drawSwipe( float x1, float y1, float x2, float y2 )
	{
		Matrix4x4 matrixBackup = GUI.matrix;
		float dir = -Mathf.Atan2( y2 - y1, x2 - x1 )*Mathf.Rad2Deg;
		float dist = Mathf.Sqrt( (x2-x1) * (x2-x1) + (y2-y1) * (y2-y1) );
		
		GUIUtility.RotateAroundPivot( dir, new Vector2(x1,Screen.height - y1));
		float height = Mathf.Min( dist*1.1f*(((float)swipeIndicator.height)) / swipeIndicator.width, Screen.height*drawScale );
		GUI.DrawTexture(new Rect( x1, Screen.height - y1 - height/2, dist*1.1f, height ), swipeIndicator);
		GUI.matrix = matrixBackup;
	}
}
