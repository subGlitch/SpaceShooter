using UnityEngine;


public class Boundaries : MonoBehaviour
{
	public static Rect		Rect;


	public static Vector2 Min		=> Rect.min;
	public static Vector2 Max		=> Rect.max;
	public static Vector2 x0y1		=> new Vector2( Rect.xMin, Rect.yMax );
	public static Vector2 x1y0		=> new Vector2( Rect.xMax, Rect.yMin );


	void Start()
	{
		Rect		= GetComponent< RectTransform >().GetWorldRect();

		CreateEdgeCollider( x0y1, Max );		// Top
		CreateEdgeCollider( Min, x1y0 );		// Bottom
		CreateEdgeCollider( Min, x0y1 );		// Left
		CreateEdgeCollider( x1y0, Max );		// Right
	}


	void CreateEdgeCollider( Vector2 p0, Vector2 p1 )
	{
		var col						= new GameObject( "Edge collider" ).AddComponent< EdgeCollider2D >();
		col.gameObject.layer		= 8;		// Boundaries layer
		col.transform.SetParent( transform );

		var points			= col.points;

		points[ 0 ]			= p0;
		points[ 1 ]			= p1;

		// (!) Important, otherwise actual points wont be updated
		col.points			= points;
	}
}

