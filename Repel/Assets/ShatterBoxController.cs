using UnityEngine;
using System.Collections;


public class ShatterBoxController : MonoBehaviour {
	public ArrayList shatters = new ArrayList();
	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	
	}

	public void add(GameObject toAdd)
	{
		shatters.Add (toAdd);
	}

	// Update is called once per frame
	void Update () {
		for( int i = 0; i < shatters.Count; i++ )
		{
			float dist = (player.transform.position - ((GameObject)shatters[i]).transform.position).sqrMagnitude;
			if( dist < 4 )
			{
				if( player.GetComponent<PlayerController>().hSpeed < 1.1 )
				{
					Application.LoadLevel( Application.loadedLevel );
				}
				else
				{
					((GameObject)shatters[i]).SetActive( false );
					shatters.RemoveAt(i);
					GameObject obj = Instantiate( Resources.Load<GameObject>( "ExplosionParticleEffect" ) ) as GameObject;
					obj.transform.position = transform.position;
					obj.GetComponent<ParticleSystem>().Play();
					i--;
					audio.PlayOneShot(audio.clip);
				}
			}
		}

	}
}
