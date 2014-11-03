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
	// Use this for initialization
	void Start () 
	{
		text = GameObject.Find ("Title");
		startPos = text.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		buttonWidth = Screen.width/numColumns;
		buttonHeight = Screen.height/numRows;

		if (!mouseWasDown && Input.GetMouseButton (0)) {
						prevY = Input.mousePosition.y;
			mouseWasDown=true;
				} else if (Input.GetMouseButton (0)) {
			yDisplacement = prevY-Input.mousePosition.y;
				} else {
			mouseWasDown =false;
				}
		Vector3 newPos = new Vector3 (startPos.x, startPos.y -(yDisplacement/Screen.height), startPos.z);
		text.gameObject.transform.position = newPos;


	}

	bool checkButton(int num, string text)
	{
		return customButton (1.5f * num, 1.5f, ""+num+") "+text);
	}


	bool customButton (float row, float column, string text)
	{
		return GUI.Button (new Rect (buttonWidth*column, buttonHeight*row + yDisplacement, buttonWidth, buttonHeight), text);
	}

	void OnGUI() 
	{
		PlayerController.hasCheck = false;
		//if (GUI.Button (new Rect (10, 10, buttonWidth/2, buttonHeight), "Back"))
		{
			//Go to the main menu
		}

		if (checkButton(3,"Tutorial: Up and up"))
		{
			Application.LoadLevel( "JumpAndDownPractice_1" );
		}

		
		if (checkButton (2,"Tutorial: Dashing along"))
		{
			Application.LoadLevel( "TalansLevel" );
		}

		if (checkButton(1, "Tutorial: New Heights")) 
		{
			Application.LoadLevel( "UpTutorial" );
		}
// 		if (checkButton (1, "Hold it!")) 
// 		{
// 			Application.LoadLevel( "testScene" );
// 		}

		if (checkButton (4, "Closed Spaces"))
		{
			Application.LoadLevel( "AJLevel" );
		}

		if (checkButton (5, "FANS")) {
			Application.LoadLevel ("AJLevel2");
		}

		if (checkButton (6, "Combined Skills")) {
			Application.LoadLevel ("AJLevel3");
		}




	}
}
