using UniRx;
using UnityEngine;


public class AsteroidView : CollidableView
{
#pragma warning disable 0649

	[SerializeField] Rigidbody2D		_rb;

#pragma warning restore 0649


	public void Init( Vector2 velocity )
	{
		_rb.velocity	 = velocity;
	}


	protected override void Start()
	{
		base.Start();

		Collisions
			.Subscribe( _ => Debug.Log( "Coll" ))
		;
	}
}

