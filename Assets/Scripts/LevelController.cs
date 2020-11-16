﻿using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelController : MB_Singleton< LevelController >
{
#region Settings

	const float ShipSpeed					= 5;

	const float AsteroidBaseSpeed			= 5;
	const float AsteroidAddonSpeed			= 4;

	const float AsteroidSpawnAreaExpand		= 1;
	const float AsteroidSpawnRightShift		= 1;

#endregion


	HashSet< ADestroyable >	_spaceObjects		= new HashSet< ADestroyable >();

	ShipModel	_ship;


	void Start()
	{
		// Spawn asteroids forever ...
	    Observable
			.Interval( TimeSpan.FromSeconds( .25f ))
			.Subscribe( _ => SpawnAsteroid() );
	}


	public void StartLevel()
	{
		SpawnShip();
	}


	void ClearLevel()
	{
		foreach (ADestroyable asteroid in _spaceObjects)
			asteroid.DestroySilently();

		_spaceObjects.Clear();
	}


	public void RestartLevel()
	{
		ClearLevel();
		StartLevel();
	}


	public void SpawnBullet()
	{
		// Create
		BulletView view					= Instantiate(
														Refs.Instance.BulletViewPrefab,
														_ship.Position.Value,
														Quaternion.identity,
														Refs.Instance.Gameplay
		);
	    BulletModel model				= new BulletModel();
		BulletController controller		= new BulletController( model, view );
		model.Fire( Vector2.right * 10 );


		// Bookkeeping
		Register( controller );
	}


	void SpawnShip()
	{
		// Create
		ShipView view					= Instantiate(
														Refs.Instance.ShipViewPrefab,
														Refs.Instance.ShipSpawnPos.position,
														Refs.Instance.ShipViewPrefab.transform.rotation,
														Refs.Instance.Gameplay
		);
	    ShipModel model					= new ShipModel( ShipSpeed );
		ShipController controller		= new ShipController( model, view );


		// Bind
		UiControllers.HudController.BindShipModel( model );
		controller.OnDestroy			+= x => UiControllers.PopupPanelController.OpenPanel();


		// Bookkeeping
		_ship							= model;
		Register( controller );
	}


	void SpawnAsteroid()
	{
		// Calc
		Vector2 expandOffset		= Vector2.up * Boundaries.Rect.height * AsteroidSpawnAreaExpand * .5f;
		Vector2 position			=
										Vector2.Lerp(
														Boundaries.x1y0	- expandOffset,
														Boundaries.Max	+ expandOffset,
														Random.value
										) +
										Vector2.right * AsteroidSpawnRightShift
		;
		Vector2 baseVelocity		= Vector2.left * AsteroidBaseSpeed;
		Vector2 addonVelocity		= Random.insideUnitCircle * AsteroidAddonSpeed;
		Vector2 velocity			= baseVelocity + addonVelocity;


		// Create
		AsteroidView view			= Instantiate(
													Refs.Instance.AsteroidViewPrefab,
													position,
													Quaternion.identity,
													Refs.Instance.Gameplay
		);
		AsteroidController controller		= new AsteroidController( view );


		// Launch
		view.SetVelocity( velocity );


		// Bookkeeping
		Register( controller );
	}


	void Register( ADestroyable controller )
	{
		_spaceObjects.Add( controller );

		controller.OnDestroy		+= x => _spaceObjects.Remove( x );
	}
}

