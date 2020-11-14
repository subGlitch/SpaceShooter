using UnityEngine;


public class BoundariesSetter : MonoBehaviour
{
	void Start()
	{
		Boundaries.Rect		= GetComponent< RectTransform >().GetWorldRect();
	}
}

