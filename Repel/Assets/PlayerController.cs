using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	bool tutWait;
	
	Swipe lastSwipe;
	float lastSwipeTime;
	EnergyController lastEnergy;
	float lastEnergyTime;
	public float mercyTime = 0.2f;
	public GameObject checkPointEffect;
	public AudioClip hitCheckpointSound;
	float hSpeedLastTouch;
	float vSpeedLastTouch;
	
	Vector3 actualPosition;
	float timeInTween;
	public float compensateTweenTime = 1;
	private bool startGame;
	
	public float airTime;
	
	public static bool hasCheck = false;
	private static Vector3 startPosition;
	
	float hSpeed;
	float vSpeed;
	bool inAir;
	bool aboveFan;
	bool belowFan;
	// Use this for initialization
	void Start () {
		timeInTween = compensateTweenTime;
		if ( !hasCheck )
		{
			startPosition = new Vector3( transform.position.x, transform.position.y, transform.position.z );
		}
		else
		{
			transform.position = new Vector3( startPosition.x, startPosition.y, startPosition.z );
			actualPosition = transform.position;
		}
		
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
	}
	
	void tapResponse( Touch t )
	{
		if( startGame && tutWait )
		{
			Debug.Log( "Touched" );
			tutWait = false;
			startGame = false;
			GetComponent<TutorialView>().unshow();
		}
	}
	
	void swipeResponse( Swipe s )
	{
		if( !startGame )
		{
			Debug.Log( "Swiped" );
			lastSwipe = s;
			lastSwipeTime = Time.time;
			Debug.Log( s.direction );
		}
	}
	
	bool cubeCollision( GameObject a, GameObject b )
	{
		//if( this->type == Shapes::CUBE && other->type == Shapes::CUBE )
		{
			float amnt;
			float mostInter;
			float closestRat;
			//glm::vec3 norm;
			
			//check the X axis
			amnt = Mathf.Abs( a.transform.position.x - b.transform.position.x ) / (a.collider.bounds.extents.x + b.collider.bounds.extents.x);
			if( amnt < 1 )
			{
				closestRat = amnt;
				//mostInter = (this->bounds.x + other->bounds.x) - closestRat * (this->bounds.x + other->bounds.x);
				//norm = glm::vec3( ( thisTrans->getTranslation().x > otherTrans->getTranslation().x ) ? 1 : -1, 0, 0 );
				//check the Y axis
				amnt = Mathf.Abs( a.transform.position.y - b.transform.position.y ) / (a.collider.bounds.extents.y + b.collider.bounds.extents.y);
				if( amnt < 1 )
				{
					if( amnt > closestRat )
					{
						closestRat = amnt;
						//mostInter = (this->bounds.y + other->bounds.y) - closestRat * (this->bounds.y + other->bounds.y);
						//norm = glm::vec3( 0, ( thisTrans->getTranslation().y > otherTrans->getTranslation().y ) ? 1 : -1, 0 );
					}
					//check the Z axis
					amnt = Mathf.Abs( a.transform.position.z - b.transform.position.z ) / (a.collider.bounds.extents.z + b.collider.bounds.extents.z);
					if( amnt < 1 )
					{
						if( amnt > closestRat )
						{
							closestRat = amnt;
							//mostInter = (this->bounds.z + other->bounds.z) - closestRat * (this->bounds.z + other->bounds.z);
							//norm = glm::vec3( 0, 0, ( thisTrans->getTranslation().z > otherTrans->getTranslation().z ) ? 1 : -1 );
						}
						
						//(*interpenetration) = mostInter + EPSILON;
						//(*collisionNormal) = norm;
						return true;
					}
				}
			}
		}
		
		return false;
	}

    public void loadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
	
	// Update is called once per frame
	void Update () {
		bool hadTouched = !inAir;
		if( !tutWait )
			inAir = true;
			
		ArrayList slopes = GameObject.Find( "LevelController" ).GetComponent<LevelController>().slopes;
		for( int i = 0; i < slopes.Count; i++ )
		{
			SlopeScript slop = ((GameObject)slopes[i]).GetComponent<SlopeScript>();
			
			if( cubeCollision( gameObject, slop.gameObject ) )
			{
				if( slop.isInRange( transform.position.x ) )
				{
					float ny = slop.getY( transform.position.x ) + 1.0f;
					
					if( ny > transform.position.y || ( airTime < 0.5 && vSpeed < 0 && Mathf.Abs( ny - transform.position.y ) < 1 ) )
					{
						if( ny > transform.position.y + 2 )
						{
							Application.LoadLevel( Application.loadedLevel );
						}
						else
						{
							transform.position = new Vector3( transform.position.x, ny, transform.position.z );
							actualPosition = transform.position;
							inAir = false;
							airTime = 0;
							vSpeed = 0;
						}
					}
				}
			}
		}
	
	
	
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
		
		
		ArrayList spikes = GameObject.Find( "LevelController" ).GetComponent<LevelController>().spikes;
		for( int i = 0; i < spikes.Count; i++ )
		{
			float dist = (transform.position - ((GameObject)spikes[i]).transform.position).sqrMagnitude;
			if( dist < 2 )
			{
			
				Application.LoadLevel( Application.loadedLevel );
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
		
		ArrayList shatters = GameObject.Find( "LevelController" ).GetComponent<LevelController>().shatters;
		for( int i = 0; i < shatters.Count; i++ )
		{
			float dist = (transform.position - ((GameObject)shatters[i]).transform.position).sqrMagnitude;
			if( dist < 4 )
			{
				if( hSpeed < 1.1 )
				{
					Application.LoadLevel( Application.loadedLevel );
				}
				else
				{
					((GameObject)shatters[i]).SetActive( false );
					shatters.RemoveAt(i);
					i--;
				}
			}
		}

		ArrayList checkpoints = GameObject.Find( "LevelController" ).GetComponent<LevelController>().checkpoints;
		for( int i = 0; i < checkpoints.Count; i++ )
		{
			float dist = (transform.position - ((GameObject)checkpoints[i]).transform.position).sqrMagnitude;
			if( dist < 6 )
			{
				if( ((GameObject)checkpoints[i]).GetComponent<CheckpointController>().goal )
				{
					Application.LoadLevel( 0 );
				}
				else
				{
					if( !((GameObject)checkpoints[i]).GetComponent<CheckpointController>().triggered )
					{
						((GameObject)checkpoints[i]).GetComponent<CheckpointController>().triggered = true;
						if( !startGame )
						{
							var partEfIn = (Instantiate(checkPointEffect,((GameObject)checkpoints[i]).transform.position,Quaternion.identity) as GameObject);
							Destroy(partEfIn,1.0f);
							audio.PlayOneShot(hitCheckpointSound,0.55f);
						}
					}
					((GameObject)checkpoints[i]).renderer.material.color = new Color( 0.5f, 1, 0 );
					startPosition = new Vector3( transform.position.x, transform.position.y, 0 );
					hasCheck = true;
				}
			}
		}
				
		if( ( (tutWait && Time.time > lastEnergyTime + 1.5f ) || ( !tutWait && Time.time - lastEnergyTime < mercyTime ) ) && Time.time - lastSwipeTime < mercyTime*2 )
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
			
			switch( lastSwipe.direction )
			{
				case Direction.UP:
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
						}
					}
					else
					{
						
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
					hSpeed = 0;
					break;
				case Direction.RIGHT:
					
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
			Vector3 off = new Vector3( 5.0f*Time.deltaTime*hSpeed, vSpeed*Time.deltaTime, 0 );
			transform.position = transform.position + off;
			actualPosition += off;
			
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
		}
	}
}
