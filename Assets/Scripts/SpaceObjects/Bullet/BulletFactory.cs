using UnityEngine;


public class BulletFactory
{
	public BulletView			View;
    public BulletModel			Model;
	public BulletController		Controller;


	public BulletFactory( ShipModel ship )
	{
		View			= GameObject.Instantiate(
													Refs.Instance.BulletViewPrefab,
													ship.Position.Value,
													Quaternion.identity,
													Refs.Instance.Gameplay
		);
	    Model			= new BulletModel();
		Controller		= new BulletController( Model, View );
	}
}

