using UnityEngine;
using System.Collections;

public class ScrollingScript : MonoBehaviour {

	float posX = 0.0f;
	float posY = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scroll ();
	}


	void scroll()
	{
		if (!transform.parent.GetComponent<PlayerController> ().tutWait) {

						posX = (posX + (Time.deltaTime * transform.parent.GetComponent<PlayerController> ().off.x));
						if (posX > 1.0f) {
								posX = -1.0f;
						}
						//posY = posY + (Time.deltaTime * transform.parent.GetComponent<PlayerController> ().off.y);
						//if (posY > 1.0f) {
						//		posY = -1.0f;
						//}
						renderer.material.mainTextureOffset = new Vector2 (posX, 0);
				}
	}
}

