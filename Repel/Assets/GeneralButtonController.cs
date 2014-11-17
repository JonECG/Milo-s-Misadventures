using UnityEngine;
using System.Collections;

public class GeneralButtonController : MonoBehaviour {

	public Texture2D unMuted;
	public Texture2D mutedTexture;

	private Texture2D current;

	private AudioSource[] sources;

	bool muted = false;
	// Use this for initialization
	void Start () {
		sources = GameObject.FindObjectsOfType<AudioSource> ();
		current = unMuted;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width-30,0 , 30, 30), current))
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
