using UnityEngine;
using System.Collections;

public class SpikeController : MonoBehaviour {

	public ArrayList spikes = new ArrayList();
	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	public void addSpike(GameObject s)
	{
		spikes.Add (s);
	}
	
	// Update is called once per frame
	void Update () {
		for( int i = 0; i < spikes.Count; i++ )
		{
			if(player!=null)
			{

				float dist = (player.gameObject.transform.position - ((GameObject)spikes[i]).transform.position).sqrMagnitude;
				if( dist < 2 )
				{
					player.GetComponent<PlayerController>().Die();
				}
			}
		}
	}
}
