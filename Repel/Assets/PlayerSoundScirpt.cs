using UnityEngine;
using System.Collections;

public class PlayerSoundScirpt : MonoBehaviour {

	private static PlayerSoundScirpt instance = null;
	public static PlayerSoundScirpt Instance {
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


	public AudioClip[] enterLevel;
	public AudioClip[] death;
	public AudioClip[] jump;
	public AudioClip[] superJump;
	public AudioClip[] dash;
	public AudioClip[] dive;
	public AudioClip[] stop;


	public void playEnterLevel()
	{
		int toPlay = (int)Random.Range (0.0f, enterLevel.Length-0.51f);
		audio.clip = enterLevel [toPlay];
		audio.Play ();
	}





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
