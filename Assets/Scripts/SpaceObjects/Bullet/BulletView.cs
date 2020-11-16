using UnityEngine;


public class BulletView : CollidableView
{
	public void SetVelocity( Vector2 velocity )
	{
		GetComponent< Rigidbody2D >().velocity		= velocity;
	}
}

