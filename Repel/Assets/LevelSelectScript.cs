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

		if (customButton ( 3, 1.5f,"Tutorial"))
		{
			Application.LoadLevel( 1 );
		}

		if (customButton ( 4.5f, 1.5f, "New Heights")) 
		{
			Application.LoadLevel( 2 );
		}

		if (customButton ( 6, 1.5f, "Closed Spaces"))
		{
			Application.LoadLevel( 3 );
		}
	}
}
