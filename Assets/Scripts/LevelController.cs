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
			.Interval( TimeSpan.FromSeconds( .25f ))
			.Subscribe( _ => Spawn() );
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

		AsteroidView view			= Instantiate( _prefab, position, Quaternion.identity );

		AsteroidController controller		= new AsteroidController( view );

		view.Init( velocity );
	}
}

