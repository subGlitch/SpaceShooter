using UnityEngine;


public class Boundaries : MonoBehaviour
{
	void Start()
	{
		Rect rect		= GetComponent< RectTransform >().GetWorldRect();

		// Top
		CreateEdgeCollider( rect,
			new Vector2( -1, 1 ),
			new Vector2( +1, 1 )
		);

		// Bottom
		CreateEdgeCollider( rect,
			new Vector2( -1, -1 ),
			new Vector2( +1, -1 )
		);

		// Left
		CreateEdgeCollider( rect,
			new Vector2( -1, -1 ),
			new Vector2( -1, +1 )
		);

		// Right
		CreateEdgeCollider( rect,
			new Vector2( 1, -1 ),
			new Vector2( 1, +1 )
		);
	}


	void CreateEdgeCollider( Rect rect, Vector2 p0, Vector2 p1 )
	{
		var col				= new GameObject( "Edge collider" ).AddComponent< EdgeCollider2D >();
		col.gameObject.layer		= 8; // Boundaries
		col.transform.SetParent( transform );

		var points			= col.points;

		Vector2 center		= rect.center;
		Vector2 halfSize	= rect.size * .5f;
		points[ 0 ]			= center + halfSize * p0;
		points[ 1 ]			= center + halfSize * p1;

		// (!) Important, otherwise actual points wont be updated
		col.points			= points;
	}
}

