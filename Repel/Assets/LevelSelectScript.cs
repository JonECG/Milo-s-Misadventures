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

	bool customButton (int row, int column, string text)
	{
		return GUI.Button (new Rect (buttonWidth*column, buttonHeight*row, buttonWidth, buttonHeight), text);
	}

	void OnGUI() 
	{
		if (GUI.Button (new Rect (10, 10, buttonWidth/2, buttonHeight), "Back"))
		{
			//Go to the main menu
		}

		if (customButton (1,1,"Tutorial"))
		{
			
		}

		if (customButton (2, 1, "JumpNDash")) 
		{

		}

		if (customButton (1,2,"Advanced"))
		{
			
		}
	}
}
