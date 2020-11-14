using UnityEngine;


public class DirKeyState : KeyState
{
	public Vector2Int Dir { get; }


	public DirKeyState( KeyCode keyCode, Vector2Int dir )
		: base( keyCode )
	{
		Dir		= dir;
	}


	public static implicit operator Vector2Int ( DirKeyState x )		=> x.Dir * x.ToInt;
}

