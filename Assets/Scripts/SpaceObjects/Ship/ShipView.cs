﻿using UniRx;
using UnityEngine;


public class ShipView : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Rigidbody2D		_rb;
	[SerializeField] CollidableView		_trigger;

#pragma warning restore 0649


	public ReadOnlyReactiveProperty< Vector2Int >	Direction;
	public ReadOnlyReactiveProperty< float >		Speed;


	public Vector2								Position		=> transform.position;
	public System.IObservable< Collider2D >		Collisions		=> _trigger.Collisions;



	void FixedUpdate()
	{
		_rb.velocity		= (Vector2)Direction.Value * Speed.Value;
	}
}

