using UnityEngine;
using System.Collections;

public class LevelSelectScript : MonoBehaviour {


	int buttonWidth = 100;
	int buttonHeight = 50;
	int numColumns = 4;
	int numRows = 10;

	float yDisplacement = 0.0f;
	bool mouseWasDown = false;
	float prevY = 0.0f; 
	Vector3 startPos; 
	GameObject text; 
	public Texture lockedTex;
	public Texture unlockedTex;
	float totalDis=0.0f;
	
	private float time;
	public Texture2D header;

	int numCols = 3;

	int currentButtonCount= 0 ; 
	int currLevel =0;
	public Texture2D buttonImage;

	private Camera cam;
	
	public bool showDebugButtons;
	
	private Texture2D black;
	// Use this for initialization
	void Start () 
	{
		black = new Texture2D(1,1);
		black.wrapMode = TextureWrapMode.Repeat;
		black.SetPixel(0,0, new Color( 0,0,0,0.7f ) );
		black.Apply();
		yDisplacement = 0.0f;
		//totalDis = -30;
		time = 0;
		
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		time += Time.deltaTime;
		cam.transform.position = new Vector3( cam.transform.position.x, yDisplacement/200 , cam.transform.position.z );

		currLevel = PlayerPrefs.GetInt ("LevelBeat");
		
		buttonWidth = Screen.width/numColumns;
		buttonHeight = Screen.height/numRows;

		if ( showDebugButtons && !mouseWasDown && Input.GetMouseButton (0)) {
						prevY = Input.mousePosition.y;
						mouseWasDown=true;
		} else if ( showDebugButtons && Input.GetMouseButton (0)) {

			yDisplacement += ((prevY-Input.mousePosition.y));
			prevY = Input.mousePosition.y;
				} else {
			mouseWasDown =false;
			//yDisplacement=0.0f;
				}
		//Debug.Log ("TotalDis: " +totalDis);
		totalDis = yDisplacement;
		//Vector3 newPos = new Vector3 (startPos.x, text.gameObject.transform.position.y -(yDisplacement/Screen.height)/2, startPos.z);
		//text.gameObject.transform.position = newPos;
		//yDisplacement = 0.0f;
		//startPos = newPos;
		
		if ( !showDebugButtons || totalDis > Screen.height / 3.5f) {
			yDisplacement-=totalDis-(Screen.height/3.5f);
			totalDis = Screen.height /3.5f;
		}
		else 
		if (totalDis + ((currentButtonCount*1.5f)*buttonHeight) < Screen.height*2.0f / 3.0f) {
			yDisplacement-= (totalDis-((Screen.height*2.0f/3.0f)-(currentButtonCount*1.5f*buttonHeight)));
			totalDis = (float)(Screen.height*2.0f /3.0f)-(currentButtonCount*1.5f*buttonHeight);
		}
	}

	bool checkButton(int num, string text)
	{
		if (num > currentButtonCount) {
			currentButtonCount=num;
		}

		bool worked = customButton ( 1.5f*(((num-1)/numCols)), 0.25f + 1.25f*(((num-1)%numCols)), text);
		if(worked)
		{
		LevelNumberHolder.currentLevel = num+1;
		PlayerController.timesDied=0; 
		}

		return worked;
	}

	bool checkButtonLock(int num, string text)
	{
		bool locked = (num <= currLevel);
		if (num > currentButtonCount) {
			currentButtonCount=num;
		}
	
		bool worked = customButtonLock (  1.5f * (((num-1)/numCols)), 0.25f + 1.25f*(((num-1)%numCols)), text,!locked);


		if(worked)
		{
		LevelNumberHolder.currentLevel = num+1;
		PlayerController.timesDied=0; 
			
		}

		
		return worked;
	}


	bool customButton (float row, float column, string text)
	{
		return GUI.Button (new Rect (buttonWidth*column, buttonHeight*row + totalDis, buttonWidth, buttonHeight), text);
	}

	bool customButtonLock (float row, float column, string text, bool locked)
	{
		bool ret = (GUI.Button (new Rect (buttonWidth*column, buttonHeight*row + totalDis, buttonWidth, buttonHeight), text) && (!locked));
		if (locked)
		{
			GUI.DrawTexture( new Rect (buttonWidth*column, buttonHeight*row + totalDis, buttonWidth, buttonHeight), black );
			GUI.DrawTexture(new Rect(buttonWidth*(column)+buttonWidth*4/5,buttonHeight*row + totalDis,buttonHeight,buttonHeight), lockedTex, ScaleMode.ScaleToFit, true);
			
		}
		else
		{
			//GUI.DrawTexture(new Rect(buttonWidth*(column)+buttonWidth*4/5,buttonHeight*row + totalDis,buttonHeight,buttonHeight), unlockedTex, ScaleMode.ScaleToFit, true);
		}
		return ret;
	}
	
	void lockButtonHelper( int position, string buttonName, string levelName )
	{
		if (checkButtonLock(position,buttonName))
		{
			ScreenTransitioner.Instance.TransitionTo( levelName );
		}
	}
	
	void OnGUI() 
	{

		GUI.skin.button.normal.background = buttonImage;
		GUI.skin.button.hover.background = buttonImage;
		GUI.skin.button.active.background = buttonImage;
		GUI.skin.button.font = Resources.Load<Font>( "UIFontRingBearer" );

		if( showDebugButtons )
		{
			if(customButton (10, 3, "DebugUnlock"))
			{
				LevelNumberHolder.setLevel(1000);
			}
			if(customButton (11, 3, "DebugLock"))
			{
				LevelNumberHolder.setLevel(0);
			}
		}



		PlayerController.hasCheck = false;
		//if (GUI.Button (new Rect (10, 10, buttonWidth/2, buttonHeight), "Back"))
		{
			//Go to the main menu
		}

		/*
		if (checkButton(1, "Tutorial: New Heights")) 
		{
			ScreenTransitioner.Instance.TransitionTo( "UpTutorial" );
		}

		int place = 2;
		
		lockButtonHelper( place++, "Tutorial: Dashing along", "TalansLevel" );
		lockButtonHelper( place++, "Tutorial: Up and up", "JumpAndDownPractice_1" );
		lockButtonHelper( place++, "Tutorial: Hold it!", "leftTutorial" );
		lockButtonHelper( place++, "Closed Spaces", "AJLevel" );
		lockButtonHelper( place++, "Fanning Out", "AJLevel2" );
		lockButtonHelper( place++, "Combined Skills", "AJLevel3" );
		lockButtonHelper( place++, "Forkroads", "ColterMidLevel" );
		lockButtonHelper( place++, "Dash up", "TalanDash" );
		lockButtonHelper( place++, "Acrobatics", "AJLevel4" );
		lockButtonHelper( place++, "Leap of Faith", "leapoffaith" );
		*/
		
		if (checkButton(1, "Up and Up")) 
		{
			ScreenTransitioner.Instance.TransitionTo( "UpTutorial" );
		}
		
		int place = 2;
		
		lockButtonHelper( place++, "Dashing along", "TalansLevel" );
		lockButtonHelper( place++, "New Heights", "JumpAndDownPractice_1" );
		lockButtonHelper( place++, "Hold it!", "leftTutorial" );
		lockButtonHelper( place++, "Closed Spaces", "AJLevel" );
		lockButtonHelper( place++, "Fanning Out", "AJLevel2" );
		lockButtonHelper( place++, "Forkroads", "ColterMidLevel" );
		lockButtonHelper( place++, "Dash up", "TalanDash" );
		lockButtonHelper( place++, "Acrobatics", "AJLevel4" );
		lockButtonHelper( place++, "Leap of Faith", "leapoffaith" );
		lockButtonHelper( place++, "Shatter Fun", "AJLevel5" );
		lockButtonHelper( place++, "Combined Skills", "AJLevel3" );
		
		
		Matrix4x4 matrixBackup = GUI.matrix;
		
		float titleWidth = Screen.width* ( 0.45f + Mathf.Sin( time ) * 0.02f );
		float titleHeight = titleWidth / (682/((float)148));
		float titleX = Screen.width/2;
		float titleY = Screen.height * ( 0.1f + Mathf.Sin( time ) * 0.01f );
		
		GUIUtility.RotateAroundPivot( Mathf.Sin( time / 2 ) * 5, new Vector2(titleX,titleY));
		GUI.DrawTexture( new Rect( titleX - titleWidth/2, titleY - titleHeight/2, titleWidth, titleHeight ), header );
		
		GUI.matrix = matrixBackup;

	}
}
