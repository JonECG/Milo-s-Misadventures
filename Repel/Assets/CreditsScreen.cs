using UnityEngine;
using System.Collections;

public class CreditsScreen : MonoBehaviour {

	public float displayTime;
	public Texture2D title, topLeft, topRight, bottomLeft, bottomRight, bottomRightCorner;
	public float titleAR, topLeftAR, topRightAR, bottomLeftAR, bottomRightAR, bottomRightCornerAR;
	
	private float time;
	
	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		
		if( time > displayTime || Input.GetMouseButtonDown( 0 ) )
		{
			ScreenTransitioner.Instance.TransitionTo( "MainMenu" );
		}
	}
	
	void drawFancyTexture( Texture2D tex, float aspect, float x, float y, float w, float angle )
	{
		Matrix4x4 matrixBackup = GUI.matrix;
		
		float calcWidth = Screen.width*w;
		float calcHeight = calcWidth / aspect;
		float calcX = Screen.width*x;
		float calcY = Screen.height*y;
		
		GUIUtility.RotateAroundPivot( angle, new Vector2(calcX,calcY));
		GUI.DrawTexture( new Rect( calcX - calcWidth/2, calcY - calcHeight/2, calcWidth, calcHeight ), tex );
		
		GUI.matrix = matrixBackup;
	}
	
	void OnGUI()
	{
		float relTime = time/2;
		drawFancyTexture( title, titleAR, 0.5f, 0.2f + Mathf.Sin( relTime * 4 ) * 0.01f, 0.4f + Mathf.Sin( relTime*4 ) * 0.05f, Mathf.Sin( relTime * 2 ) * 8 );
		
		
		relTime += 30.8f;
		drawFancyTexture( topLeft, topLeftAR, 0.25f, 0.5f + Mathf.Sin( relTime * 4 ) * 0.005f, 0.4f + Mathf.Sin( relTime*4 ) * 0.02f, Mathf.Sin( relTime * 2 ) * 4 );
		relTime *= 0.8f;
		relTime += 10.2f;
		drawFancyTexture( topRight, topRightAR, 0.75f, 0.5f + Mathf.Sin( relTime * 4 ) * 0.005f, 0.4f + Mathf.Sin( relTime*4 ) * 0.02f, Mathf.Sin( relTime * 2 ) * 4 );
		relTime *= 1.3f;
		relTime += 20.6f;
		drawFancyTexture( bottomLeft, bottomLeftAR, 0.25f, 0.7f + Mathf.Sin( relTime * 4 ) * 0.005f, 0.4f + Mathf.Sin( relTime*4 ) * 0.02f, Mathf.Sin( relTime * 2 ) * 4 );
		relTime *= 0.9f;
		relTime += 30.7f;
		drawFancyTexture( bottomRight, bottomRightAR, 0.75f, 0.7f + Mathf.Sin( relTime * 4 ) * 0.005f, 0.4f + Mathf.Sin( relTime*4 ) * 0.02f, Mathf.Sin( relTime * 2 ) * 4 );
		
		float logoWidth = Screen.width*0.3f;
		float logoHeight = logoWidth / bottomRightCornerAR;
		GUI.DrawTexture( new Rect( Screen.width - logoWidth, Screen.height - logoHeight, logoWidth, logoHeight ), bottomRightCorner );
	}
}
