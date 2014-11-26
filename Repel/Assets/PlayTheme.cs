using UnityEngine;
using System.Collections;

public class PlayTheme : MonoBehaviour {

	public AudioClip nextMusic;

	public float fadeTime = 0.3f;
	public float volumeScale = 0.5f;
	
	bool isPlaying = false;
	bool isTransitioning = false;
	bool fadingOut;

	private static PlayTheme instance = null;
	
	private float transitioningTime;
	public static PlayTheme Instance 
	{
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
	
	public void TransitionPlay( AudioClip music )
	{
		if( !isTransitioning )
		{			
			if( music != null )
			{
				if( !isPlaying )
				{
					audio.clip = music;
					isPlaying = true;
					audio.loop = true;
					audio.volume = volumeScale;
					audio.Play();
					audio.time = 0;
				}
				else
				if( music != audio.clip )
				{
					isTransitioning = true;
					fadingOut = true;
					transitioningTime = 0;
					nextMusic = music;
				}
			}
		}
	}
		
	public void TransitionPlay( string name )
	{
		if( !isTransitioning )
		{
			AudioClip loaded = Resources.Load<AudioClip>( name );
			
			TransitionPlay( loaded );
		}
	}
	
	public void TransitionStop()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		if( isTransitioning )
		{
			transitioningTime += Time.deltaTime;
			
			if( fadingOut )
			{
				audio.volume = volumeScale * Mathf.Clamp( ( fadeTime - transitioningTime ) / fadeTime, 0, 1 );
			}
			else
			{
				audio.volume = volumeScale*(1-Mathf.Clamp( ( fadeTime - transitioningTime ) / fadeTime, 0, 1 ));
			}
			
			if( transitioningTime > fadeTime )
			{
				if( fadingOut )
				{
					if( nextMusic != null )
					{
						audio.Stop();
						audio.clip = nextMusic;
						audio.time = 0;
						audio.Play();
						nextMusic = null;
						fadingOut = false;
					}
					else
					{
						audio.Stop();
						isPlaying = false;
						isTransitioning = false;
					}
				}
				else
				{
					isTransitioning = false;
				}
				
				transitioningTime = 0;
			}
		}
	}


}
