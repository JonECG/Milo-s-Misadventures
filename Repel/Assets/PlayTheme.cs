using UnityEngine;
using System.Collections;

public class PlayTheme : MonoBehaviour {

	public AudioClip theme;

	private static PlayTheme instance = null;
	public static PlayTheme Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}



	// Use this for initialization
	void Start () {
		audio.loop = true;
		audio.Play ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
