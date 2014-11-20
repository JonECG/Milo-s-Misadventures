using UnityEngine;
using System.Collections;

public class ShatterBoxSound : MonoBehaviour {

	
	private static ShatterBoxSound instance = null;
	public static ShatterBoxSound Instance {
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


	public void playSound()
	{
		audio.Play ();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
