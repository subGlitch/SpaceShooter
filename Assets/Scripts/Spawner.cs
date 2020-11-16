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
}

