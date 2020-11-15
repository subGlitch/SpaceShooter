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

#endregion


	HashSet< IDestroyable >	_asteroids		= new HashSet< IDestroyable >();


	void Start()
	{
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
		foreach (IDestroyable asteroid in _asteroids)
			asteroid.DestroySilently();

		_asteroids.Clear();
	}


	public void RestartLevel()
	{
		ClearLevel();
		StartLevel();
	}


	void SpawnShip()
	{
		ShipView view					= Instantiate(
														Refs.Instance.ShipViewPrefab,
														Refs.Instance.ShipSpawnPos.position,
														Refs.Instance.ShipViewPrefab.transform.rotation,
														Refs.Instance.Gameplay
		);
	    ShipModel model					= new ShipModel( ShipSpeed );
		ShipController controller		= new ShipController( model, view );

		UiControllers.HudController.BindShipModel( model );

		model.OnDestroyed				+= () => UiControllers.PopupPanelController.OpenPanel();
	}


	void SpawnAsteroid()
	{
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

		AsteroidView view			= Instantiate(
													Refs.Instance.AsteroidViewPrefab,
													position,
													Quaternion.identity,
													Refs.Instance.Gameplay
		);

		AsteroidController controller		= new AsteroidController( view );
		controller.OnDestroy		+= x => _asteroids.Remove( x );

		view.Init( velocity );

		_asteroids.Add( controller );
	}
}

