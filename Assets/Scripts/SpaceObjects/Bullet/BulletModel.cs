using UniRx;
using UnityEngine;


public class BulletModel
{
	public readonly ReactiveProperty< Vector2 >		Velocity			= new ReactiveProperty< Vector2 >();


	public void Fire( Vector2 velocity )
	{
		Velocity.Value		= velocity;
	}
}

