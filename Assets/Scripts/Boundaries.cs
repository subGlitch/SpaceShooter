using UnityEngine;


public class Boundaries : MonoBehaviour
{
	static Rect		_rect;


	public static Vector2 Min		=> _rect.min;
	public static Vector2 Max		=> _rect.max;
	public static Vector2 x0y1		=> new Vector2( _rect.xMin, _rect.yMax );
	public static Vector2 x1y0		=> new Vector2( _rect.xMax, _rect.yMin );


	void Start()
	{
		_rect		= GetComponent< RectTransform >().GetWorldRect();

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

