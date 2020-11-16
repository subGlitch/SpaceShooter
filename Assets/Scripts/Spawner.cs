using UnityEngine;


public static class Spawner
{
	static Settings Settings		=> Refs.Instance.Settings;


	public static BulletFactory SpawnBullet( Vector2 position )
	{
		// Create
		BulletFactory factory		= new BulletFactory( position );
		
		// Set velocity
		factory.Model.Fire( Vector2.right * Settings.BulletsSpeed );

		// Bookkeeping
		Bookkeeper.Register( factory.Controller );
		
		return factory;
	}


	public static ShipFactory SpawnShip()
	{
		// Create
		ShipFactory factory			= new ShipFactory( Settings.ShipSpeed );

		// Bookkeeping
		Bookkeeper.Register( factory.Controller );
		
		return factory;
	}


	public static AsteroidFactory SpawnAsteroid()
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

		return factory;
	}

}

