using UnityEngine;


public class AsteroidView : TriggerView
{
#pragma warning disable 0649

	[SerializeField] Rigidbody2D	_rb;

#pragma warning restore 0649


	public void Init( Vector2 velocity )
	{
		_rb.velocity	 = velocity;
	}
}

