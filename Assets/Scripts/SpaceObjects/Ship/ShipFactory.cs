using UnityEngine;


public class ShipFactory
{
	public ShipView			View;
	public ShipController	Controller;
    public ShipModel		Model;
	

	public ShipFactory( float speed )
	{
		View				= GameObject.Instantiate(
														Refs.Instance.ShipViewPrefab,
														Refs.Instance.ShipSpawnPos.position,
														Refs.Instance.ShipViewPrefab.transform.rotation,
														Refs.Instance.Gameplay
		);
	    Model				= new ShipModel( speed );
		Controller			= new ShipController( Model, View );
	}
}

