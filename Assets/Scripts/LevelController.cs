using System;
using UniRx;
using UnityEngine;


public class LevelController : MonoBehaviour
{
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
		AsteroidView asteroid		= Instantiate( _prefab );

		asteroid.Init( Vector2.left );
	}
}

