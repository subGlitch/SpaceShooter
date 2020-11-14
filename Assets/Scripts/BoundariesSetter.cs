using UnityEngine;


public class BoundariesSetter : MonoBehaviour
{
	void Start()
	{
		Rect rect			= GetComponent< RectTransform >().GetWorldRect();
		Boundaries.Rect		= rect;

		var top				= new GameObject().AddComponent< EdgeCollider2D >();

		var points			= top.points;
		points[ 0 ]			= rect.center + rect.size * .5f * new Vector2( -1, 1 );
		points[ 1 ]			= rect.center + rect.size * .5f * new Vector2( +1, 1 );

		// (!) Important, otherwise actual points wont be updated
		top.points			= points;
	}
}

