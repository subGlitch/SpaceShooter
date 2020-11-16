using UnityEngine;


public class BulletFactory
{
	public BulletView			View;
	public BulletController		Controller;
    public BulletModel			Model;


	public BulletFactory( Vector3 position )
	{
		View			= GameObject.Instantiate(
													Refs.Instance.BulletViewPrefab,
													position,
													Quaternion.identity,
													Refs.Instance.Gameplay
		);
	    Model			= new BulletModel();
		Controller		= new BulletController( Model, View );
	}
}

