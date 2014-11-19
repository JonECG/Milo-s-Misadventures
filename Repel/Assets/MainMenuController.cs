using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public static bool hasVisitedMainMenu = false, differentSceneRedirect = false;
	public static string reloadTarget = "";
	
	// Use this for initialization
	void Start () {
		hasVisitedMainMenu = true;
		if( differentSceneRedirect )
		{
			differentSceneRedirect = true;
			Application.LoadLevel( reloadTarget );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
