using UniRx.Triggers;
using UnityEngine;


public class PlayerTriggerView : MonoBehaviour
{
	public System.IObservable< Collider2D >		AsteroidCollisions;


	void Start()
	{
		AsteroidCollisions		= gameObject.OnTriggerEnter2DAsObservable();
	}
}

