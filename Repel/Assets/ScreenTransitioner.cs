using UnityEngine;
using System.Collections;

public class ScreenTransitioner : MonoBehaviour {

	public static ScreenTransitioner Instance;
	
	private string target;
	private float progress;
	
	public float rate = 1;
	
	public Material transitionMaterial = null;
	
	
	// Use this for initialization
	void Start () {
		Instance = this;
		progress = 0;
		target = null;
		
		if( transitionMaterial == null )
		{
			transitionMaterial = new Material( Shader.Find( "Diffuse" ) );
			transitionMaterial.color = new Color( 0,0,0 );
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown( KeyCode.A ) )
		{
			TransitionTo( Application.loadedLevelName );
		}
	
		if( target == null )
			progress = Mathf.Min( 1, progress + rate * Time.deltaTime );
		else
			progress -= rate * Time.deltaTime;
			
		if( target != null && progress < 0 )
		{
			Application.LoadLevel( target );
		}
		
	}
	
	public void TransitionTo( string levelName )
	{
		if( target == null )
			target = levelName;
	}
	
	void OnGUI()
	{
		GUI.depth = -1000;
		float show = Mathf.Clamp( progress, 0, 1 );
		GL.PushMatrix();
		GL.LoadOrtho();
		
		
		for( var i = 0; i < transitionMaterial.passCount; i++ )
		{
			transitionMaterial.SetPass( i );
			GL.Color( new Color( 0,0,0 ) );
			GL.Begin( GL.TRIANGLES );
			
			if( target == null )
			{
				GL.Vertex3( -1 + show, 0-show, 0.1f );
				GL.Vertex3( +1 + show, 2-show, 0.1f );
				GL.Vertex3( +1 + show, 0-show, 0.1f );
			}
			else
			{
				GL.Vertex3( +0 - show, +1+show, 0.1f );
				GL.Vertex3( +2 - show, +1+show, 0.1f );
				GL.Vertex3( +0 - show, -1+show, 0.1f );
			}
			
			GL.End();
		}
		
		GL.PopMatrix();
	}
}
