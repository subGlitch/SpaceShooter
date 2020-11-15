using UniRx.Triggers;
using UnityEngine;


public class CollidableView : MonoBehaviour
{
	public System.IObservable< Collider2D >		Collisions;


	protected virtual void Start()
	{
		Collisions		= gameObject.OnTriggerEnter2DAsObservable();
	}
}

