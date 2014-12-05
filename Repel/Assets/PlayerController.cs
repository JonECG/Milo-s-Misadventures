using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool tutWait;
	
	Swipe lastSwipe;
	float lastSwipeTime;
	EnergyController lastEnergy;
	float lastEnergyTime;
	public float mercyTime = 0.2f;

	float hSpeedLastTouch;
	float vSpeedLastTouch;

	public GameObject deathParticle;
	
	public Vector3 actualPosition;
	public float timeInTween;
	public float compensateTweenTime = 1;
	public bool startGame;
	public AudioClip swipeSound;
	
	public float airTime;
	
	public static bool hasCheck = false;
	public static Vector3 startPosition;
	public static int timesDied = 0; 
	
	
	public float hSpeedComponent = 5.0f;
	public float hSpeed;
	public float vSpeed;
	public bool inAir;
	public Vector3 off;
	bool aboveFan;
	bool belowFan;

	GUIContent restartContect = new GUIContent();
	public Texture2D buttonImage;
	string restartText = "Restart";
	GUIContent mainMenuContect = new GUIContent();
	string mainMenuText = "MainMenu";
	private bool levelDone =false;
	
	ShockwaveController shock;


	private int buttonRows = 6;
	private int buttonColumns = 6;
	private int buttonHeight;
	private int buttonWidth;


	// Use this for initialization
	void Start () {
		dying = false;
		deathTimer = 0.0f;
		int i;
		if( !MainMenuController.hasVisitedMainMenu )
		{
			MainMenuController.differentSceneRedirect = true;
			MainMenuController.reloadTarget = Application.loadedLevelName;
			Application.LoadLevel( "MainMenu" );
		}
		else
		{
			timeInTween = compensateTweenTime;
			if ( !hasCheck )
			{
				PlayerSoundScript.Instance.playEnterLevel();
				startPosition = new Vector3( transform.position.x, transform.position.y, transform.position.z );
			}
			else
			{
				transform.position = new Vector3( startPosition.x, startPosition.y, startPosition.z );
				actualPosition = transform.position;
			}
			
			shock = GameObject.Find( "ShockwaveEffect" ).GetComponent<ShockwaveController>();
			
			startGame = true;
			vSpeed = 0;
			hSpeed = 1;
			airTime = 0;
			inAir = true;
			lastSwipeTime = -50;
			lastEnergyTime = -50;
			tutWait = true;
			aboveFan = false;
			belowFan = false;
			GetComponent<TutorialView>().show( new Vector2( 0.75f, 0.75f ), new Vector2( 0.75f, 0.75f ), "Tap to begin" );
			GetComponent<TapAndSlash>().Subscribe( tapResponse, swipeResponse );
	
			//restartContect.image = buttonImage;
			restartContect.text = restartText;
			//mainMenuContect.image = buttonImage;
			mainMenuContect.text = mainMenuText;
		}
	}
	
	void tapResponse( Touch t )
	{
		if( levelDone )
		{
			loadSelector();
		}
		if( startGame && tutWait && !levelDone )
		{
			Debug.Log( "Touched" );
			tutWait = false;
			startGame = false;
			GetComponent<TutorialView>().unshow();
		}
	}
	
	void swipeResponse( Swipe s )
	{
		tapResponse( new Touch() );
		if( !startGame && !levelDone )
		{
			Debug.Log( "Swiped" );
			lastSwipe = s;
			lastSwipeTime = Time.time;

			audio.PlayOneShot(swipeSound,3.0f);
			Debug.Log( s.direction );
		}
	}

	bool dying =false;
	public void Die()
	{
		if (!dying) 
		{
			hSpeed = 0;
			vSpeed = 0;
			this.gameObject.renderer.enabled = false;
			
			GameObject.Find( "default" ).renderer.enabled = false;
			shock.KillShockwave();
			
			PlayerSoundScript.Instance.playEnterDeath();
	
			GameObject g = (Instantiate (deathParticle, this.gameObject.transform.position, new Quaternion ()) as GameObject);
			g.GetComponent<ParticleSystem> ().Play();
	
			timesDied++;
			dying = true;
				
		}




		//yield return new WaitForSeconds (1.0f);

		//Application.LoadLevel( Application.loadedLevel );
	}


	public void completeLevel()
	{
		levelDone = true;
		tutWait = true;
		string timey = "";
		if (timesDied != 1) {
			timey+="s";
				}
		string ender = "Congratulations! You won!\nYou died " + timesDied+" time"+timey+"!\n Tap to return back to the level select."; 
		GetComponent<TutorialView>().show( new Vector2( 0.75f, 0.75f ), new Vector2( 0.75f, 0.75f ), ender );
		//GetComponent<TutorialView>().show( new Vector2( 0.75f, 0.75f ), new Vector2( 0.75f, 0.75f ), "Tap to begin" );
	}

	public void loadSelector()
	{
		ScreenTransitioner.Instance.TransitionTo( "LevelSelect" );
	}

    public void loadLevel()
    {
		ScreenTransitioner.Instance.TransitionTo( Application.loadedLevelName );
    }

	float deathTimer = 0.0f;
	// Update is called once per frame
	void Update () {
		bool hadTouched = !inAir;
	

		if (dying) {
			deathTimer+=Time.deltaTime;
			if(deathTimer>0.5)
			{
				ScreenTransitioner.Instance.TransitionTo( Application.loadedLevelName );
			}

		}
		else
		{
	
			ArrayList en = GameObject.Find( "LevelController" ).GetComponent<LevelController>().energies;
			for( int i = 0; i < en.Count; i++ )
			{
				EnergyController energ = ((GameObject)en[i]).GetComponent<EnergyController>();
				
				if( energ.gameObject.activeSelf )
				{
					float dist = (transform.position - energ.transform.position).sqrMagnitude;
					if( dist > energ.lastDistance && dist < 0.5 )
					{
						lastEnergyTime = Time.time;
						lastEnergy = energ;
						vSpeedLastTouch = vSpeed;
						hSpeedLastTouch = hSpeed;
						energ.touched = true;
						touchEnergy( ((GameObject)en[i]) );
					}
					energ.lastDistance = dist;
				}
			}
	
			aboveFan = false;
			belowFan = false;
			ArrayList fans = GameObject.Find( "LevelController" ).GetComponent<LevelController>().fans;
			for( int i = 0; i < fans.Count; i++ )
			{
				if (transform.position.x > ((GameObject)fans[i]).transform.position.x-2)
				{
					if (transform.position.x < ((GameObject)fans[i]).transform.position.x+2)
					{
						if (transform.position.y >= ((GameObject)fans[i]).transform.position.y)
						{
							aboveFan = true;
						}
						else
						{
							belowFan = true;
						}
					}
				}
			}
			
		
					
			if( lastEnergy != null && ( (tutWait && Time.time > lastEnergyTime + 1.5f ) || ( !tutWait && Time.time - lastEnergyTime < mercyTime ) ) && Time.time - lastSwipeTime < mercyTime*2 )
			{
				tutWait = false;
				GetComponent<TutorialView>().unshow();
				
				lastEnergyTime = -50;
				lastSwipeTime = -50;
				timeInTween = 0;
				actualPosition = lastEnergy.transform.position;
				hSpeed = hSpeedLastTouch;
				vSpeed = vSpeedLastTouch;
				transform.FindChild("particle").GetComponent<ParticleSystem>().Play();
				
				GameObject.Find( "Mageshiro" ).GetComponent<Animator>().Play( "ThrustMagic", -1, 0 );
				GameObject.Find( "Mageshiro" ).GetComponent<Animator>().speed = 1;
				
				switch( lastSwipe.direction )
				{
					case Direction.UP:
						PlayerSoundScript.Instance.playJump();
						shock.DisplayShockwave( new Vector3( 0, -1, 0 ), 0.2f );
						if (aboveFan && belowFan)
						{
							vSpeed = 10;
						}
						else
						if (aboveFan)
						{
							vSpeed = 15;
						}
						else
						if (belowFan)
						{
							vSpeed = 6;
						}
						else
						{
							vSpeed = 10;
						}
						inAir = true;
						break;
					case Direction.DOWN:
						if( airTime > 0.5)
						{
							if (!inAir)
							{
								PlayerSoundScript.Instance.playSuperJump();
								shock.DisplayShockwave( new Vector3( 1, 12, 0 ), 1 );
								if (aboveFan && belowFan)
								{
									vSpeed = 14;
								}
								else
									if (aboveFan)
								{
									vSpeed = 20;
								}
								else
									if (belowFan)
								{
									vSpeed = 10;
								}
								else
								{
									vSpeed = 14;
								}
								hSpeed = 1;
							}
							else
							{
								PlayerSoundScript.Instance.playDive();
								shock.DisplayShockwave( new Vector3( 1, -10, 0 ), 1 );
								if (aboveFan && belowFan || inAir)
								{
									vSpeed = -15;
								}
								else
									if (aboveFan)
								{
									vSpeed = -10;
								}
								else
									if (belowFan)
								{
									vSpeed = -20;
								}
								else
								{
									vSpeed = -15;
								}
								hSpeed = 1;
							}
						}
						else
						{
							PlayerSoundScript.Instance.playSuperJump();
							shock.DisplayShockwave( new Vector3( 1, 12, 0 ), 1 );
							if (aboveFan)
							{
								vSpeed = 20;
							}
							else
								if (belowFan)
							{
								vSpeed = 10;
							}
							else
							{
								vSpeed = 14;
							}
							hSpeed = 1;
							inAir = true;
							airTime = 20;
							hSpeed = 0.25f;
							
							
						}
						break;
					case Direction.LEFT:
						PlayerSoundScript.Instance.playStop();
						shock.DisplayShockwave( new Vector3( 1, 0, 0 ), 0.2f );
						hSpeed = 0;
						break;
					case Direction.RIGHT:
						PlayerSoundScript.Instance.playDash();
						shock.DisplayShockwave( new Vector3( 1, 0, 0 ), 0.8f );
						hSpeed = 3;
						if (aboveFan)
						{
							vSpeed = 2;
						}
						else
						if (belowFan)
						{
							vSpeed = -2;
						}
						else
						{
							vSpeed = 0;
						}
						break;
				}
				lastEnergy.gameObject.SetActive( false );
			}
			
			if( !tutWait )
			{
				airTime+=Time.deltaTime;
				off = new Vector3( hSpeedComponent*Time.deltaTime*hSpeed, vSpeed*Time.deltaTime, 0 );
				transform.position = transform.position + off;
				actualPosition += off;
				
				if( !inAir )
					GameObject.Find( "Mageshiro" ).GetComponent<Animator>().speed = hSpeed;
				
				if( timeInTween < compensateTweenTime )
				{
					transform.position = transform.position + (actualPosition-transform.position) * timeInTween/compensateTweenTime;
					timeInTween += Time.deltaTime;
				}
				
				if( inAir && hSpeed < 2.5 )
				{
					vSpeed -= 9*Time.deltaTime;
				}
				else
				{
					if( hSpeed < 1 )
						hSpeed += Mathf.Min( 1 - hSpeed, 0.7f*Time.deltaTime );
				}
				if( hSpeed > 1 )
					hSpeed -= Mathf.Min( hSpeed - 1, 1.8f*Time.deltaTime );
				if (transform.position.y < -10 )
				{
					Application.LoadLevel( Application.loadedLevel );
				}
			}
			else
			{
				GameObject.Find( "Mageshiro" ).GetComponent<Animator>().speed = 0;
			}
			
			if (!tutWait) {
				inAir = true;
			}
		}
	}
	
	void touchEnergy(GameObject theCollision)
	{
		Debug.Log( theCollision.name );
		if( theCollision.name == "Energy" )
		{
			EnergyController en = theCollision.GetComponent<EnergyController>();
			if( en.tutorial )
			{
				GetComponent<TutorialView>().show( en.tutDirection, en.tutorialMessage );
				tutWait = true;
				transform.FindChild("particle").GetComponent<ParticleSystem>().Pause();
				lastSwipeTime = -50;
			}
			GetComponent<TapAndSlash>().InterruptMouseDown();
		}
	}

    void OnGUI()
    {
		GUI.skin.button.normal.background = buttonImage;
		GUI.skin.button.hover.background = buttonImage;
		GUI.skin.button.active.background = buttonImage;

		buttonWidth = Screen.width / buttonRows;
		buttonHeight = Screen.height / buttonColumns;

		if (GUI.Button(new Rect(10,Screen.height -buttonHeight , buttonWidth, buttonHeight), restartContect))
        {
            loadLevel();
        }
		if (GUI.Button(new Rect(10+buttonWidth,Screen.height -buttonHeight , buttonWidth, buttonHeight), mainMenuContect))
		{
			loadSelector();
		}
    }
}
