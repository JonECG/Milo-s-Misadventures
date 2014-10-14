using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	bool tutWait;
	
	Swipe lastSwipe;
	float lastSwipeTime;
	EnergyController lastEnergy;
	float lastEnergyTime;
	public float mercyTime = 0.2f;
	
	float hSpeedLastTouch;
	float vSpeedLastTouch;
	
	Vector3 actualPosition;
	float timeInTween;
	public float compensateTweenTime = 1;
	
	public static bool hasCheck = false;
	private static Vector3 startPosition;
	
	float hSpeed;
	float vSpeed;
	bool inAir;
	// Use this for initialization
	void Start () {
		if ( !hasCheck )
		{
			startPosition = new Vector3( transform.position.x, transform.position.y, transform.position.z );
		}
		else
		{
			transform.position = new Vector3( startPosition.x, startPosition.y, startPosition.z );
			actualPosition = transform.position;
		}
		
		vSpeed = 0;
		hSpeed = 1;
		inAir = false;
		lastSwipeTime = -50;
		lastEnergyTime = -50;
		tutWait = false;
		GetComponent<TapAndSlash>().Subscribe( null, swipeResponse );
	}
	
	void swipeResponse( Swipe s )
	{
		Debug.Log( "Swiped" );
		lastSwipe = s;
		lastSwipeTime = Time.time;
		Debug.Log( s.direction );
	}
	
	// Update is called once per frame
	void Update () {
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
					((GameObject)checkpoints[i]).renderer.material.color = new Color( 0.5f, 1, 0 );
					startPosition = new Vector3( transform.position.x, 1.5f, 0 );
					hasCheck = true;
				}
			}
		}
				
		if( ( (tutWait && Time.time > lastEnergyTime + 1.5f ) || ( !tutWait && Time.time - lastEnergyTime < mercyTime ) ) && Time.time - lastSwipeTime < mercyTime*2 )
		{
			lastEnergyTime = -50;
			lastSwipeTime = -50;
			tutWait = false;
			timeInTween = 0;
			actualPosition = lastEnergy.transform.position;
			hSpeed = hSpeedLastTouch;
			vSpeed = vSpeedLastTouch;
			transform.FindChild("particle").GetComponent<ParticleSystem>().Play();
			GetComponent<TutorialView>().unshow();
			
			switch( lastSwipe.direction )
			{
				case Direction.UP:
					vSpeed = 10;
					inAir = true;
					break;
				case Direction.DOWN:
					if( inAir )
					{
						vSpeed = -15;
						hSpeed = 1;
					}
					else
					{
						inAir = true;
						hSpeed = 0.25f;
						vSpeed = 14;
					}
					break;
				case Direction.LEFT:
					hSpeed = 0;
					break;
				case Direction.RIGHT:
					hSpeed = 3;
					vSpeed = 0;
					break;
			}
			lastEnergy.gameObject.SetActive( false );
		}
		
		if( !tutWait )
		{
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
			if (transform.position.y < 1.5 )
			{
				vSpeed = 0;
				inAir = false;
				transform.position = new Vector3( transform.position.x, 1.5f, transform.position.z );
				actualPosition = new Vector3( actualPosition.x, 1.5f, actualPosition.z );
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
