using UnityEngine;


[CreateAssetMenu( fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig" )]
public class LevelConfig : ScriptableObject
{
	public float	Time;
	public float	AsteroidsRate;

	[Header( "Will be set automatically during game start" )]
	public int		Seed;
}

