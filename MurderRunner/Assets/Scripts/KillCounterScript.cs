using UnityEngine;
using System.Collections;

public class KillCounterScript : MonoBehaviour {

	public static int KillCount = 0;
	public GUIText KillCounter;
	// Use this for initialization
	void Start () 
	{
		
	}

	public static void Increment()
	{
		KillCount++;
	}
	
	// Update is called once per frame
	void Update () 
	{
		KillCounter.text = "Kill Count: " + KillCount.ToString ();
	}
}
