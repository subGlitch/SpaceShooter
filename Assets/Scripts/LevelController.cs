using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelController : MB_Singleton< LevelController >
{
	const float BaseSpeed		= 5;
	const float AddonSpeed		= 4;


	HashSet< AsteroidController >		_asteroids		= new HashSet< AsteroidController >();


	void Start()
	{
	    Observable
			.Interval( TimeSpan.FromSeconds( .25f ))
			.Subscribe( _ => Spawn() );
	}


	public void StartLevel()
	{
		
	}


	public void CloseLevel()
	{
		foreach (AsteroidController asteroid in _asteroids)
			asteroid.DestroySilently();

		_asteroids.Clear();
	}


	void Spawn()
	{
		const float expand			= 1;
		const float shiftRight		= 1;

		Vector2 expandOffset		= Vector2.up * Boundaries.Rect.height * expand * .5f;
		Vector2 position			=
										Vector2.Lerp(
														Boundaries.x1y0	- expandOffset,
														Boundaries.Max	+ expandOffset,
														Random.value
										) +
										Vector2.right * shiftRight
		;

		Vector2 baseVelocity		= Vector2.left * BaseSpeed;
		Vector2 addonVelocity		= Random.insideUnitCircle * AddonSpeed;
		Vector2 velocity			= baseVelocity + addonVelocity;

		AsteroidView view			= Instantiate( Refs.Instance.AsteroidViewPrefab, position, Quaternion.identity );

		AsteroidController controller		= new AsteroidController( view );
		controller.OnDestroy		+= x => _asteroids.Remove( x );

		view.Init( velocity );

		_asteroids.Add( controller );
	}
}

