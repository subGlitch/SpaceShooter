using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelController : MonoBehaviour
{
	const float BaseSpeed		= 5;
	const float AddonSpeed		= 4;


#pragma warning disable 0649

	[SerializeField] AsteroidView		_prefab;

#pragma warning restore 0649


	void Start()
	{
	    Observable
			.Interval( TimeSpan.FromSeconds( 1 ))
			.Subscribe( _ => Spawn() );
	}


	void Spawn()
	{
		Vector2 baseVelocity		= Vector2.left * BaseSpeed;
		Vector2 addonVelocity		= Random.insideUnitCircle * AddonSpeed;
		Vector2 velocity			= baseVelocity + addonVelocity;

		AsteroidView asteroid		= Instantiate( _prefab );

		asteroid.Init( velocity );
	}
}

