using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public static bool hasVisitedMainMenu = false, differentSceneRedirect = false;
	public static string reloadTarget = "";
	
	
	public Texture2D title, tap;
	
	private float time;
	
	// Use this for initialization
	void Start () {
		hasVisitedMainMenu = true;
		if( differentSceneRedirect )
		{
			differentSceneRedirect = true;
			Application.LoadLevel( reloadTarget );
		}
		
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
	}
	
	void OnGUI()
	{
		Matrix4x4 matrixBackup = GUI.matrix;
		//float dir = -Mathf.Atan2( y2 - y1, x2 - x1 )*Mathf.Rad2Deg;
		//float dist = Mathf.Sqrt( (x2-x1) * (x2-x1) + (y2-y1) * (y2-y1) );
		
		float titleWidth = Screen.width* ( 0.95f + Mathf.Sin( time*4 ) * 0.05f );
		float titleHeight = titleWidth / (800/((float)347));
		float titleX = Screen.width/2;
		float titleY = Screen.height * ( 0.4f + Mathf.Sin( time * 4 ) * 0.02f );
		
		GUIUtility.RotateAroundPivot( Mathf.Sin( time * 2 ) * 8, new Vector2(titleX,titleY));
		GUI.DrawTexture( new Rect( titleX - titleWidth/2, titleY - titleHeight/2, titleWidth, titleHeight ), title );
		
		GUI.matrix = matrixBackup;
		
		float tapWidth = Screen.width* ( 0.7f + Mathf.Sin( time ) * 0.01f );
		float tapHeight = tapWidth / (796/((float)81));
		float tapX = Screen.width/2;
		float tapY = Screen.height * ( 0.9f + Mathf.Sin( time * 2 ) * 0.03f );
		
		//GUIUtility.RotateAroundPivot( Mathf.Sin( time ) * 2, new Vector2(tapX,tapY));
		GUI.DrawTexture( new Rect( tapX - tapWidth/2, tapY - tapHeight/2, tapWidth, tapHeight ), tap );
		
		//float height = Mathf.Min( dist*1.1f*(((float)swipeIndicator.height)) / swipeIndicator.width, Screen.height*drawScale );
		//GUI.DrawTexture(new Rect( x1, Screen.height - y1 - height/2, dist*1.1f, height ), swipeIndicator);
		GUI.matrix = matrixBackup;
	}
}
