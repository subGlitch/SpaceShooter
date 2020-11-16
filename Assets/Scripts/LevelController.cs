using System;
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
	public Subject< int >	LevelCompletedEvents		= new Subject< int >();


	static int	_asteroidsOnScreen;


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
									.Interval( TimeSpan.FromSeconds( Refs.Instance.Settings.AsteroidsSpawnRate ))
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
		Bookkeeper.Clear();

		_asteroidSpawner	.Dispose();
		_timer				.Dispose();

		_ship		= null;
	}


	public void SpawnBullet()
	{
		BulletFactory factory			= new BulletFactory( _ship.Position.Value );
		
		// Set velocity
		factory.Model.Fire( Vector2.right * Refs.Instance.Settings.BulletsSpeed );

		// Bookkeeping
		Bookkeeper.Register( factory.Controller );
	}


	void SpawnShip()
	{
		// Create
		ShipFactory factory			= new ShipFactory( Refs.Instance.Settings.ShipSpeed );


		// Bind
		UiControllers.HudController.BindShipModel( factory.Model );
		factory.Controller.OnDestroy		+= x => Transition( LevelState.Fail );


		// Bookkeeping
		_ship		= factory.Model;
		Bookkeeper.Register( factory.Controller );
	}


	void SpawnAsteroid()
	{
		// Calc
		Vector2 expandOffset		= Vector2.up * Boundaries.Rect.height * Refs.Instance.Settings.AsteroidSpawnAreaExpand * .5f;
		Vector2 position			=
										Vector2.Lerp(
														Boundaries.x1y0	- expandOffset,
														Boundaries.Max	+ expandOffset,
														Random.value
										) +
										Vector2.right * Refs.Instance.Settings.AsteroidSpawnRightShift
		;
		Vector2 baseVelocity		= Vector2.left * Refs.Instance.Settings.AsteroidBaseSpeed;
		Vector2 addonVelocity		= Random.insideUnitCircle * Refs.Instance.Settings.AsteroidAddonSpeed;
		Vector2 velocity			= baseVelocity + addonVelocity;


		// Create
		AsteroidFactory factory		= new AsteroidFactory( position );


		// Launch
		factory.Model.Launch( velocity );


		// Bookkeeping
		Bookkeeper.Register( factory.Controller );
	}
}

