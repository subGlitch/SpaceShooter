using UniRx.Triggers;
using UnityEngine;


public class CollidableView : MonoBehaviour
{
	public System.IObservable< Collider2D >		Collisions;


	void Awake()
	{
		Collisions		= gameObject.OnTriggerEnter2DAsObservable();
	}
}

