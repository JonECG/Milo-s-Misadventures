using UnityEngine;
using System.Collections;

public class LevelNumberHolder : MonoBehaviour {

	public static int currentLevel;

	private static LevelNumberHolder instance = null;
	public static LevelNumberHolder Instance {
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



	public static void setHighestLevel()
	{
		int currHighestLevel = PlayerPrefs.GetInt ("LevelBeat");
		if(currentLevel>=currHighestLevel)
		{
			PlayerPrefs.SetInt("LevelBeat",currentLevel);
			PlayerPrefs.Save();
		}
	}
	public static void setLevel(int num)
	{
		PlayerPrefs.SetInt("LevelBeat",num);
		PlayerPrefs.Save();
	}




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
