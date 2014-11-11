using UnityEngine;
using System.Collections;

public class Crusher : MonoBehaviour {

    private Vector3 upPosition;
    public Vector3 downPosition;
    private bool movingDown = true;
    private float travel = 0.0f;
    public float changeAmount = 0.01f;
    public GameObject player;
    public int distanceTillActivation = 10;
	// Use this for initialization
	void Start () {
        downPosition = this.transform.position;
        downPosition.y -= 10;
        upPosition = this.transform.position;
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceTillActivation)
        {
            if (movingDown)
            {
                transform.position = Vector3.Lerp(upPosition, downPosition, travel);
                if (Vector3.Distance(transform.position, downPosition) < 0.5f)
                {
                    travel = 0.0f;
                    movingDown = !movingDown;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(downPosition, upPosition, travel);
                if (Vector3.Distance(transform.position, upPosition) < 0.5f)
                {
                    travel = 0.0f;
                    movingDown = !movingDown;
                }
            }
            travel += changeAmount;
        }
	}
}
