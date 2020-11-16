using UnityEngine;


public class RigidView : TriggerView
{
#pragma warning disable 0649

	[SerializeField] Rigidbody2D	_rb;

#pragma warning restore 0649


	public void SetVelocity( Vector2 velocity )
	{
		_rb.velocity		= velocity;
	}

}
