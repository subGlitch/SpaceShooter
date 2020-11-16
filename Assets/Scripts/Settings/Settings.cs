using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Settings", menuName = "ScriptableObjects/Settings" )]
public class Settings : ScriptableObject
{
	[Header( "Ship settings" )]
	public float ShipSpeed;

	[Header( "Asteroids settings" )]
	public float AsteroidBaseSpeed;
	public float AsteroidAddonSpeed;
	public float AsteroidSpawnAreaExpand;
	public float AsteroidSpawnRightShift;
	public float AsteroidsSpawnRate;

	[Header( "Bullets settings" )]
	public float BulletsSpeed;

	[Header( "Levels" )]
	public List< LevelConfig >		Levels;
}

