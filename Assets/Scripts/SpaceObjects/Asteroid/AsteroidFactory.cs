using UnityEngine;


public class AsteroidFactory
{
	public AsteroidView			View;
	public AsteroidController	Controller;
	public AsteroidModel		Model;


	public AsteroidFactory( Vector3 position )
	{
		View			= GameObject.Instantiate(
													Refs.Instance.AsteroidViewPrefab,
													position,
													Quaternion.identity,
													Refs.Instance.Gameplay
		);
		Model			= new AsteroidModel();
		Controller		= new AsteroidController( Model, View );
	}
}

