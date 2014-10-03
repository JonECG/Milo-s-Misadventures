using UnityEngine;
using System.Collections;

public class MouseDragMovement : MonoBehaviour {

	public float speed = 5.0f; 
	public Vector2 mouseDirection;
	private bool is2DGame = true;

	private string playerName = "CubeShuro";

	private bool playerWasClicked = false;

	private bool MouseWasDown = false;
	// Use this for initialization
	void Start () {
		is2DGame = Camera.main.gameObject.transform.rotation.eulerAngles.x <= 45;

	}
	
	// Update is called once per frame
	void Update () {
		Ray test = Camera.main.ScreenPointToRay (Input.mousePosition);
		bool mouseIsClicked = Input.GetMouseButton (0);
		Vector3 mouseWorldPosition = test.origin; //Camera.main.ScreenToWorldPoint(Input.mousePosition);
						
		if (!MouseWasDown||playerWasClicked) {
						if (mouseIsClicked) {

								if (!playerWasClicked) {
										resolveIfPlayerHit ();
								} else if (playerWasClicked) {
										//Debug.Log ("mouseWorldPositiony X: " + mouseWorldPosition.x + " , Y: " + mouseWorldPosition.y + " , Z: " + mouseWorldPosition.z);
				

										Vector3 newVelocity = (mouseWorldPosition - this.gameObject.transform.position); 
										newVelocity.y = 0;
										if (is2DGame) {
												newVelocity.z = 0; 
										}
						
										//Debug.Log ("New Velocity X: " + newVelocity.x + " , Y: " + newVelocity.y + " , Z: " + newVelocity.z);
						
								
						
						
										this.gameObject.rigidbody.velocity = Vector3.Normalize (newVelocity) * speed;
								}
						} else if (playerWasClicked) {
								playerWasClicked = false;
						}
				}
		MouseWasDown = mouseIsClicked;
		

	}

	private void resolveIfPlayerHit()
	{
		RaycastHit hit = new RaycastHit ();
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 200)) {
			resolveCollision (hit);
		}
	}

	private void resolveCollision(RaycastHit hit)
	{
		//Debug.Log("Had a Collision: " + hit.collider.gameObject.name);

		GameObject wasHit = hit.collider.gameObject;


		if(wasHit.name == playerName)
		{
			playerWasClicked = true;
		}


//		if (currentCam != toolBar) {
//			curr = hit.collider.gameObject;
//			active = true;
//		} else {
//			GameObject g = hit.collider.gameObject; 
//			curr = Instantiate(g, g.transform.position, g.transform.rotation) as GameObject;
//			active = true;
//		}
	}

}
