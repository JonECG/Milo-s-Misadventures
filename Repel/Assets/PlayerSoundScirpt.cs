using UnityEngine;
using System.Collections;

public class PlayerSoundScript : MonoBehaviour {

	private static PlayerSoundScript instance = null;
	public static PlayerSoundScript Instance {
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
		playMeSound (enterLevel);

	}

	public void playEnterDeath()
	{
		playMeSound (death);
		
	}
	
	public void playJump()
	{
		playMeSound (jump);

	}


	public void playSuperJump()
	{
		playMeSound (superJump);
		
	}
	
	public void playDash()
	{
		playMeSound (dash);
	}


	public void playDive()
	{
		playMeSound (dive);
	}

	public void playStop()
	{
		playMeSound (stop);
	}


	public void playMeSound(AudioClip[] array)
	{
		if(!audio.isPlaying)
		{
			int toPlay = (int)Random.Range (0.0f, array.Length-0.51f);
			audio.clip = array [toPlay];
			audio.Play ();
		}
	}





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
