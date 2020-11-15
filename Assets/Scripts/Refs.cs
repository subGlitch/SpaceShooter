using UnityEngine;


public class Refs : MB_Singleton< Refs >
{
	[Header( "Prefabs" )]
	public ShipView			ShipViewPrefab;
	public AsteroidView		AsteroidViewPrefab;

	[Header( "Refs" )]
	public Transform		ShipSpawnPos;
}

