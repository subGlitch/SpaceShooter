using UnityEngine;


public static class Spawner
{
	public static BulletFactory SpawnBullet( Vector2 position )
	{
		BulletFactory factory		= new BulletFactory( position );
		
		// Set velocity
		factory.Model.Fire( Vector2.right * Refs.Instance.Settings.BulletsSpeed );

		// Bookkeeping
		Bookkeeper.Register( factory.Controller );
		
		return factory;
	}
}

