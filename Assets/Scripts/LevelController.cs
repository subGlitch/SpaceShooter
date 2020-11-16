using System;
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
	const float AsteroidsSpawnRate			= .25f;

	const float BulletsSpeed				= 10;

#endregion


	HashSet< ADestroyableController >	_spaceObjects		= new HashSet< ADestroyableController >();

	ShipModel		_ship;

	IDisposable		_asteroidSpawner;


	// Test
	void Update()
	{
		if (Input.GetKeyDown( KeyCode.Space ))
			_asteroidSpawner.Dispose();
	}


	public void StartLevel()
	{
		SpawnShip();

		_asteroidSpawner		= Observable
									.Interval( TimeSpan.FromSeconds( AsteroidsSpawnRate ))
									.Subscribe( _ => SpawnAsteroid() );
	}


	void ClearLevel()
	{
		foreach (ADestroyableController asteroid in _spaceObjects)
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
		BulletFactory factory			= new BulletFactory( _ship.Position.Value );
		
		// Set velocity
		factory.Model.Fire( Vector2.right * BulletsSpeed );

		// Bookkeeping
		Register( factory.Controller );
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
		AsteroidFactory factory		= new AsteroidFactory( position );


		// Launch
		factory.Model.Launch( velocity );


		// Bookkeeping
		Register( factory.Controller );
	}


	void Register( ADestroyableController controller )
	{
		_spaceObjects.Add( controller );

		controller.OnDestroy		+= x => _spaceObjects.Remove( x );
	}
}

