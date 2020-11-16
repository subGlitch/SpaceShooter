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


	public Subject< int >	LevelCompletedEvents		= new Subject< int >();


	static int	_asteroidsOnScreen;


	HashSet< ADestroyableController >	_spaceObjects		= new HashSet< ADestroyableController >();

	ShipModel		_ship;

	IDisposable		_asteroidSpawner;
	IDisposable		_timer;

	LevelState		_state;
	int				_levelIndex;


	public ReactiveProperty< float >	TimerEndTime		= new ReactiveProperty< float >();


	public void ChangeAsteroidsOnScreenCount( int delta )
	{
		_asteroidsOnScreen		+= delta;

		CheckConditions();
	}


	void Transition( LevelState state )
	{
		if (
				_state == LevelState.Fail &&
				state == LevelState.TimeOut
			)
			return;

		_state		= state;

		switch (state)
		{
			case LevelState.None:
				UiControllers.HudController.SetActive( false );
				break;

			case LevelState.InProcess:
				UiControllers.HudController.SetActive( true );
				break;

			case LevelState.TimeOut:
				CheckConditions();
				break;

			case LevelState.Fail:
				UiControllers.PopupPanelController.OpenPanel( false );
				break;

			case LevelState.Win:
				UiControllers.PopupPanelController.OpenPanel( true );
				LevelCompletedEvents.OnNext( _levelIndex );
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


	void OnTimerOut()
	{
		_asteroidSpawner.Dispose();
		Transition( LevelState.TimeOut );
	}


	public void StartLevel( int levelIndex )
	{
		_levelIndex					= levelIndex;
		LevelConfig levelConfig		= Refs.Instance.Settings.Levels[ levelIndex ];

		Random.InitState( levelConfig.Seed );

		SpawnShip();

		_asteroidSpawner		= Observable
									.Interval( TimeSpan.FromSeconds( AsteroidsSpawnRate ))
									.Subscribe( _ => SpawnAsteroid() );


		float time				= levelConfig.Time;
		TimerEndTime.Value		= Time.time + time;

		_timer					= Observable
									.Timer( TimeSpan.FromSeconds( time ))
									.Subscribe( _ => OnTimerOut() );

		Transition( LevelState.InProcess );
	}


	public void CloseLevel()
	{
		ClearLevel();
		Transition( LevelState.None );
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

