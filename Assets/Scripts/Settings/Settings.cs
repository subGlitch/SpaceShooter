using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( fileName = "Settings", menuName = "ScriptableObjects/Settings" )]
public class Settings : ScriptableObject
{
	public List< LevelConfig >		Levels;
}

