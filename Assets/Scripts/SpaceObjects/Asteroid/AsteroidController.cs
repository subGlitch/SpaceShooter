using UniRx;
using UnityEngine;


public class AsteroidController
{
	public AsteroidController( AsteroidView view )
	{
		view.Collisions
				.Subscribe( _ => GameObject.Destroy( view.gameObject ) );
	}
}

