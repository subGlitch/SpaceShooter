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

	public ReactiveProperty< float >	TimerEndTime		= new ReactiveProperty< float >();


	static int	_asteroidsOnScreen;


	IDisposable		_asteroidSpawner;
	IDisposable		_timer;

	LevelState		_state;
	int				_levelIndex;


	public void StartLevel( int levelIndex )
	{
		_levelIndex					= levelIndex;
		LevelConfig levelConfig		= Refs.Instance.Settings.Levels[ levelIndex ];
		Random.InitState( levelConfig.Seed );


		// Spawn Ship
		SpawnShip();


		// Init Asteroid spawning
		_asteroidSpawner		= Observable
									.Interval( TimeSpan.FromSeconds( Refs.Instance.Settings.AsteroidsSpawnRate ))
									.Subscribe( _ => Spawner.SpawnAsteroid() );


		// Init Level Timer
		float time				= levelConfig.Time;
		TimerEndTime.Value		= Time.time + time;
		_timer					= Observable
									.Timer( TimeSpan.FromSeconds( time ))
									.Subscribe( _ => OnTimerOut() );


		// Change State
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
	}


	public void ChangeAsteroidsOnScreenCount( int delta )
	{
		_asteroidsOnScreen		+= delta;

		CheckConditions();
	}


#region Finite State Machine

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

#endregion	


	void OnTimerOut()
	{
		_asteroidSpawner.Dispose();
		Transition( LevelState.TimeOut );
	}


	void SpawnShip()
	{
		// Create
		ShipFactory factory			= Spawner.SpawnShip();

		// Bind
		UiControllers.HudController.BindShipModel( factory.Model );
		factory.Controller.OnDestroy		+= x => Transition( LevelState.Fail );
	}
}

