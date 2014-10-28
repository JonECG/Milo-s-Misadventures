using UnityEngine;
using System.Collections;

public class LevelSelectScript : MonoBehaviour {


	int buttonWidth = 100;
	int buttonHeight = 50;
	int numColumns = 4;
	int numRows = 10;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		buttonWidth = Screen.width/numColumns;
		buttonHeight = Screen.height/numRows;
	}

	bool checkButton(int num, string text)
	{
		return customButton (1.5f * num, 1.5f, ""+num+") "+text);
	}


	bool customButton (float row, float column, string text)
	{
		return GUI.Button (new Rect (buttonWidth*column, buttonHeight*row, buttonWidth, buttonHeight), text);
	}

	void OnGUI() 
	{
		PlayerController.hasCheck = false;
		//if (GUI.Button (new Rect (10, 10, buttonWidth/2, buttonHeight), "Back"))
		{
			//Go to the main menu
		}

		if (checkButton(2,"Up and up"))
		{
			Application.LoadLevel( "JumpAndDownPractice_1" );
		}

		
		if (checkButton (3,"Dashing along"))
		{
			Application.LoadLevel( "TalansLevel" );
		}

		if (checkButton(4, "New Heights")) 
		{
			Application.LoadLevel( "UpTutorial" );
		}

		if (checkButton (1, "Hold it!")) 
		{
			Application.LoadLevel( "testScene" );
		}

		if (checkButton (5, "Closed Spaces"))
		{
			Application.LoadLevel( "AJLevel" );
		}







	}
}
