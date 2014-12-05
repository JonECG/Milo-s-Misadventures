using UnityEngine;
using System.Collections;

public class GeneralButtonController : MonoBehaviour {

	public Texture2D unMuted;
	public Texture2D mutedTexture;

	private Texture2D current;

	private AudioSource[] sources;

	private static bool muted = false;
	// Use this for initialization
	void Start () {
		sources = GameObject.FindObjectsOfType<AudioSource> ();
		current = (GeneralButtonController.muted) ? mutedTexture : unMuted;
		if (GeneralButtonController.muted) {
			muteAll();
				}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void muteAll()
	{
		for(int i = 0 ; i<sources.Length;i++)
		{
			sources[i].mute=muted;
		}
	}

	void OnGUI()
	{
		int tempWidth = Screen.width / 12;
		int tempHeight = Screen.height / 12;
		int actualWidth = 0;

		if (tempHeight > tempWidth)
		{
			actualWidth = tempHeight;
		}
		else
		{
			actualWidth = tempWidth;
		}

		if (GUI.Button(new Rect(Screen.width-actualWidth,0 , actualWidth, actualWidth), current))
		{
			muted=!muted;
			
			for(int i = 0 ; i<sources.Length;i++)
			{
				sources[i].mute=muted;
			}
			if(muted)
			{
				current = mutedTexture;
			}
			else{
				current = unMuted;
			}


		}

	}


}
