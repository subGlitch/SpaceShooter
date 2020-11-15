using UniRx.Triggers;
using UnityEngine;


public class ShipTriggerView : MonoBehaviour
{
	public System.IObservable< Collider2D >		AsteroidCollisions;


	void Start()
	{
		AsteroidCollisions		= gameObject.OnTriggerEnter2DAsObservable();
	}
}

