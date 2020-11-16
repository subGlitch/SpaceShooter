using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


public enum LevelState
{
	None,

	InProcess,
	TimeOut,
	Fail,
	Win,
}


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

	const float LevelTime					= 10;

#endregion


	static int	_asteroidsOnScreen;


	HashSet< ADestroyableController >	_spaceObjects		= new HashSet< ADestroyableController >();

	ShipModel		_ship;

	IDisposable		_asteroidSpawner;
	IDisposable		_timer;

	LevelState		_state;


	public ReactiveProperty< float >	TimerEndTime		= new ReactiveProperty< float >();


	public void ChangeAsteroidsOnScreenCount( int delta )
	{
		_asteroidsOnScreen		+= delta;

		CheckConditions();
	}


	void Transition( LevelState state )
	{
		_state		= state;

		switch (state)
		{
			case LevelState.InProcess:
				break;

			case LevelState.TimeOut:
				_asteroidSpawner.Dispose();
				CheckConditions();
				break;

			case LevelState.Fail:
				UiControllers.PopupPanelController.OpenPanel( false );
				break;

			case LevelState.Win:
				UiControllers.PopupPanelController.OpenPanel( true );
				break;
		}
	}


	void CheckConditions()
	{
		switch (_state)
		{
			case LevelState.TimeOut:
				if (_asteroidsOnScreen == 0)
					Transition( LevelState.Win );
				break;
		}
	}


	public void StartLevel()
	{
		SpawnShip();

		_asteroidSpawner		= Observable
									.Interval( TimeSpan.FromSeconds( AsteroidsSpawnRate ))
									.Subscribe( _ => SpawnAsteroid() );


		float time				= LevelTime;
		TimerEndTime.Value		= Time.time + time;

		_timer					= Observable
									.Timer( TimeSpan.FromSeconds( time ))
									.Subscribe( _ => Transition( LevelState.TimeOut ) );

		Transition( LevelState.InProcess );
	}


	void ClearLevel()
	{
		foreach (ADestroyableController spaceObject in _spaceObjects)
			spaceObject.DestroySilently();
		_spaceObjects.Clear();

		_asteroidSpawner	.Dispose();
		_timer				.Dispose();

		_ship		= null;
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
		ShipFactory factory			= new ShipFactory( ShipSpeed );


		// Bind
		UiControllers.HudController.BindShipModel( factory.Model );
		factory.Controller.OnDestroy		+= x => Transition( LevelState.Fail );


		// Bookkeeping
		_ship		= factory.Model;
		Register( factory.Controller );
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

