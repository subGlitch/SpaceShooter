using UnityEngine;


public class BulletView : TriggerView
{
	public void SetVelocity( Vector2 velocity )
	{
		GetComponent< Rigidbody2D >().velocity		= velocity;
	}
}

