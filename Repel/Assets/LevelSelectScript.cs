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

	int currentButtonCount= 0 ; 
	int currLevel =0;
	// Use this for initialization
	void Start () 
	{
		yDisplacement = 0.0f;
		//totalDis = -30;
		text = GameObject.Find ("Title");
		
		startPos = text.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{


		currLevel = PlayerPrefs.GetInt ("LevelBeat");
		
		buttonWidth = Screen.width/numColumns;
		buttonHeight = Screen.height/numRows;

		if (!mouseWasDown && Input.GetMouseButton (0)) {
						prevY = Input.mousePosition.y;
						mouseWasDown=true;
				} else if (Input.GetMouseButton (0)) {

			yDisplacement += ((prevY-Input.mousePosition.y));
			prevY = Input.mousePosition.y;
				} else {
			mouseWasDown =false;
			//yDisplacement=0.0f;
				}
		Debug.Log ("TotalDis: " +totalDis);
		totalDis = yDisplacement;
		//Vector3 newPos = new Vector3 (startPos.x, text.gameObject.transform.position.y -(yDisplacement/Screen.height)/2, startPos.z);
		//text.gameObject.transform.position = newPos;
		//yDisplacement = 0.0f;
		//startPos = newPos;
		text.gameObject.SetActive(false);

		if (totalDis > Screen.height / 8.0f) {
			yDisplacement-=totalDis-(Screen.height/8.0f);
			totalDis = Screen.height /8.0f;
		}
		else if (totalDis + ((currentButtonCount*1.5f)*buttonHeight) < Screen.height*2.0f / 3.0f) {
			yDisplacement-= (totalDis-((Screen.height*2.0f/3.0f)-(currentButtonCount*1.5f*buttonHeight)));
			totalDis = (float)(Screen.height*2.0f /3.0f)-(currentButtonCount*1.5f*buttonHeight);
		}
		




	}

	bool checkButton(int num, string text)
	{
		if (num > currentButtonCount) {
			currentButtonCount=num;
		}

		bool worked = customButton (1.5f * num, 1.5f, ""+num+") "+text);
		if(worked)
		{
		LevelNumberHolder.currentLevel = num+1;
		}

		return worked;
	}

	bool checkButtonLock(int num, string text)
	{
		bool locked = (num <= currLevel);
		if (num > currentButtonCount) {
			currentButtonCount=num;
		}

		if (locked) {
			bool stopHere=false;
				}
		else{
			bool stopHere = false;
		}

		bool worked = customButtonLock (1.5f * num, 1.5f, ""+num+") "+text,!locked);
		if(worked)
		{
		LevelNumberHolder.currentLevel = num+1;
		}

		
		return worked;
	}


	bool customButton (float row, float column, string text)
	{
		return GUI.Button (new Rect (buttonWidth*column, buttonHeight*row + totalDis, buttonWidth, buttonHeight), text);
	}

	bool customButtonLock (float row, float column, string text, bool locked)
	{
		if (locked)
		{
			GUI.DrawTexture(new Rect(buttonWidth*(column-1),buttonHeight*row + totalDis,buttonHeight,buttonHeight), lockedTex, ScaleMode.ScaleToFit, true);
		}
		else
		{
			GUI.DrawTexture(new Rect(buttonWidth*(column-1),buttonHeight*row + totalDis,buttonHeight,buttonHeight), unlockedTex, ScaleMode.ScaleToFit, true);
		}
		return (GUI.Button (new Rect (buttonWidth*column, buttonHeight*row + totalDis, buttonWidth, buttonHeight), text) && (!locked));
	}

	void OnGUI() 
	{

		if(customButton (10, 3, "DebugUnlock"))
		{
			LevelNumberHolder.setLevel(1000);
		}
		if(customButton (11, 3, "DebugLock"))
		{
			LevelNumberHolder.setLevel(0);
		}



		PlayerController.hasCheck = false;
		//if (GUI.Button (new Rect (10, 10, buttonWidth/2, buttonHeight), "Back"))
		{
			//Go to the main menu
		}


		if (checkButton(1, "Tutorial: New Heights")) 
		{
			Application.LoadLevel( "UpTutorial" );
		}

		
		if (checkButtonLock(2,"Tutorial: Dashing along"))
		{
			Application.LoadLevel( "TalansLevel" );
		}

		if (checkButtonLock(3,"Tutorial: Up and up"))
		{
			Application.LoadLevel( "JumpAndDownPractice_1" );
		}
		if (checkButtonLock(4,"Tutorial: Hold it!"))
		{
			Application.LoadLevel( "leftTutorial" );
		}
		if (checkButtonLock (5, "Closed Spaces"))
		{
			Application.LoadLevel( "AJLevel" );
		}
		
		if (checkButtonLock (6, "FANS")) {
			Application.LoadLevel ("AJLevel2");
		}
		
		if (checkButtonLock (7, "Combined Skills")) {
			Application.LoadLevel ("AJLevel3");
		}
		
		if (checkButtonLock (8, "Forkroads")) {
			Application.LoadLevel ("ColterMidLevel");
		}
		
		if (checkButtonLock (9, "Dash up")) {
			Application.LoadLevel ("TalanDash");
		}
		
		if (checkButtonLock (10, "Acrobatics")) {
			Application.LoadLevel ("AJLevel4");
		}
		
		if (checkButtonLock (11, "Leap of Faith")) {
			Application.LoadLevel ("leapoffaith");
		}

// 		if (checkButton (1, "Hold it!")) 
// 		{
// 			Application.LoadLevel( "testScene" );
// 		}






	}
}
