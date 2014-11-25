using UnityEngine;
using System.Collections;

public class MusicStarter : MonoBehaviour {

	public AudioClip music;
	
	// Use this for initialization
	void Update () {
		if( PlayTheme.Instance != null )
			PlayTheme.Instance.TransitionPlay( music );
	}
}
