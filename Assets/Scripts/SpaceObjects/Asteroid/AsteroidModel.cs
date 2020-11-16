using UniRx;
using UnityEngine;


public class AsteroidModel
{
	public readonly ReactiveProperty< Vector2 >		Velocity			= new ReactiveProperty< Vector2 >();


	public void Launch( Vector2 velocity )
	{
		Velocity.Value		= velocity;
	}
}

