using UnityEngine;
using System.Collections;

public class SlopeScript : MonoBehaviour {

	public float leftPerc = 0.5f, rightPerc = 1;
	
	// Use this for initialization
	void Start () {
		GenMesh();
		GameObject.Find( "LevelController" ).GetComponent<LevelController>().slopes.add ( this.gameObject );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool isInRange( float x )
	{
		float rat = (x - transform.position.x + collider.bounds.extents.x)/(2*collider.bounds.extents.x);
		return (rat >= 0 && rat <= 1);
	}
	
	public float getY( float x )
	{
		float rat = Mathf.Clamp( (x - transform.position.x + collider.bounds.extents.x)/(2*collider.bounds.extents.x), 0, 1 );
		float interp = leftPerc + ( rightPerc-leftPerc ) * rat;
		return transform.position.y + ( interp * 2 - 1 ) * collider.bounds.extents.y;
	}
	
	void GenMesh()
	{
		MeshFilter filter = (GetComponent(typeof( MeshFilter ) ) as MeshFilter);
		Mesh mesh = filter.mesh;
		
		Vector3[] vertices = new Vector3[24];
		int[] indices = new int[36];
		Vector2[] uvs = new Vector2[24];
		
		int vertexCount = 0;
		int indexCount = 0;
		
		//Top
		makePlane( new Vector3( -0.5f, leftPerc - 0.5f, 0.5f ), new Vector3( 0.5f, rightPerc - 0.5f, 0.5f ), new Vector3(-0.5f, leftPerc - 0.5f, -0.5f ), new Vector3(0.5f, rightPerc - 0.5f, -0.5f ),
			vertices, indices, uvs, ref vertexCount, ref indexCount );
		//Bottom
		makePlane( new Vector3(-0.5f, -0.5f, -0.5f ), new Vector3(0.5f, -0.5f, -0.5f ), new Vector3( -0.5f, -0.5f, 0.5f ), new Vector3( 0.5f, -0.5f, 0.5f ),
			vertices, indices, uvs, ref vertexCount, ref indexCount );
		//Left
		makePlane( new Vector3( -0.5f, leftPerc - 0.5f, 0.5f ), new Vector3( -0.5f, leftPerc - 0.5f, -0.5f ), new Vector3(-0.5f, -0.5f, 0.5f ), new Vector3(-0.5f, -0.5f, -0.5f ),
		    vertices, indices, uvs, ref vertexCount, ref indexCount );
		//Right
		makePlane( new Vector3(0.5f, -0.5f, 0.5f ), new Vector3(0.5f, -0.5f, -0.5f ), new Vector3( 0.5f, rightPerc - 0.5f, 0.5f ), new Vector3( 0.5f, rightPerc - 0.5f, -0.5f ),
			vertices, indices, uvs, ref vertexCount, ref indexCount );
		//Front
		makePlane( new Vector3(-0.5f, leftPerc-0.5f, -0.5f ), new Vector3(0.5f, rightPerc-0.5f, -0.5f ), new Vector3( -0.5f, -0.5f, -0.5f ), new Vector3( 0.5f, -0.5f, -0.5f ),
		    vertices, indices, uvs, ref vertexCount, ref indexCount );
		//Back
		makePlane( new Vector3( -0.5f, -0.5f, 0.5f ), new Vector3( 0.5f, -0.5f, 0.5f ), new Vector3(-0.5f, leftPerc-0.5f, 0.5f ), new Vector3(0.5f, rightPerc-0.5f, 0.5f ),
		    vertices, indices, uvs, ref vertexCount, ref indexCount );
		
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = indices;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
	
	void makePlane( Vector3 tl, Vector3 tr, Vector3 bl, Vector3 br, Vector3[] vertices, int[] indices, Vector2[] uvs, ref int currentVertex, ref int currentIndex )
	{
		indices[currentIndex+0] = currentVertex;
		indices[currentIndex+1] = currentVertex+1;
		indices[currentIndex+2] = currentVertex+2;
		indices[currentIndex+3] = currentVertex+2;
		indices[currentIndex+4] = currentVertex+1;
		indices[currentIndex+5] = currentVertex+3;
		
		vertices[currentVertex+0] = tl;
		vertices[currentVertex+1] = tr;
		vertices[currentVertex+2] = bl;
		vertices[currentVertex+3] = br;
		
		uvs[currentVertex+0] = new Vector2( 0, 0 );
		uvs[currentVertex+1] = new Vector2( 1, 0 );
		uvs[currentVertex+2] = new Vector2( 0, 1 );
		uvs[currentVertex+3] = new Vector2( 1, 1 );
		
		currentVertex += 4;
		currentIndex += 6;
	}
}
