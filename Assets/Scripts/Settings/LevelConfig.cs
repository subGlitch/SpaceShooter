using System;
using UnityEngine;


[Serializable]
public struct LevelConfig
{
	public float	Time;
	public float	AsteroidsRate;

	[Header( "Will be set automatically during game start" )]
	public int		Seed;
}

