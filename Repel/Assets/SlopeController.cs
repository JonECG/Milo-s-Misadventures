using UnityEngine;
using System.Collections;

public class SlopeController : MonoBehaviour {
	public ArrayList slopes = new ArrayList();
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	
	}

	public void add(GameObject toAdd)
	{
		slopes.Add (toAdd);
	}

	bool cubeCollision( GameObject a, GameObject b )
	{
		//if( this->type == Shapes::CUBE && other->type == Shapes::CUBE )
		{
			float amnt;
			float mostInter;
			float closestRat;
			//glm::vec3 norm;
			
			//check the X axis
			amnt = Mathf.Abs( a.transform.position.x - b.transform.position.x ) / (a.collider.bounds.extents.x + b.collider.bounds.extents.x);
			if( amnt < 1 )
			{
				closestRat = amnt;
				//mostInter = (this->bounds.x + other->bounds.x) - closestRat * (this->bounds.x + other->bounds.x);
				//norm = glm::vec3( ( thisTrans->getTranslation().x > otherTrans->getTranslation().x ) ? 1 : -1, 0, 0 );
				//check the Y axis
				amnt = Mathf.Abs( a.transform.position.y - b.transform.position.y ) / (a.collider.bounds.extents.y + b.collider.bounds.extents.y);
				if( amnt < 1 )
				{
					if( amnt > closestRat )
					{
						closestRat = amnt;
						//mostInter = (this->bounds.y + other->bounds.y) - closestRat * (this->bounds.y + other->bounds.y);
						//norm = glm::vec3( 0, ( thisTrans->getTranslation().y > otherTrans->getTranslation().y ) ? 1 : -1, 0 );
					}
					//check the Z axis
					amnt = Mathf.Abs( a.transform.position.z - b.transform.position.z ) / (a.collider.bounds.extents.z + b.collider.bounds.extents.z);
					if( amnt < 1 )
					{
						if( amnt > closestRat )
						{
							closestRat = amnt;
							//mostInter = (this->bounds.z + other->bounds.z) - closestRat * (this->bounds.z + other->bounds.z);
							//norm = glm::vec3( 0, 0, ( thisTrans->getTranslation().z > otherTrans->getTranslation().z ) ? 1 : -1 );
						}
						
						//(*interpenetration) = mostInter + EPSILON;
						//(*collisionNormal) = norm;
						return true;
					}
				}
			}
		}
		
		return false;
	}

	
	// Update is called once per frame
	void Update () {
		
		for( int i = 0; i < slopes.Count; i++ )
		{
			SlopeScript slop = ((GameObject)slopes[i]).GetComponent<SlopeScript>();
			
			if( cubeCollision( player.gameObject, slop.gameObject ) )
			{
				if( slop.isInRange( player.transform.position.x ) )
				{
					float ny = slop.getY( player.transform.position.x ) + 1.0f;
					
					if( ny > player.transform.position.y || ( player.GetComponent<PlayerController>().airTime < 0.5 && player.GetComponent<PlayerController>().vSpeed < 0 && Mathf.Abs( ny - player.transform.position.y ) < 1 ) )
					{
						if( ny > player.transform.position.y + 2 )
						{
							if( slop.isShatterable && ( player.GetComponent<PlayerController>().hSpeed > 1.1 || ( Mathf.Abs( player.GetComponent<PlayerController>().vSpeed ) > 10 ) ) )
							{
								Destroy( slop.gameObject );
								slopes.RemoveAt( i );
								i--;
							}
							else
								Application.LoadLevel( Application.loadedLevel );
						}
						else
						{
							if( slop.isShatterable && Mathf.Abs( player.GetComponent<PlayerController>().vSpeed ) > 15 )
							{
								Destroy( slop.gameObject );
								slopes.RemoveAt( i );
								i--;
							}
							else
							{
								player.transform.position = new Vector3( player.transform.position.x, ny, player.transform.position.z );
								player.GetComponent<PlayerController>().actualPosition = player.transform.position;
								player.GetComponent<PlayerController>().inAir = false;
								player.GetComponent<PlayerController>().airTime = 0;
								player.GetComponent<PlayerController>().vSpeed = 0;
							}
						}
					}
				}
			}
		}
	}
}
