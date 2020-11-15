using UniRx;
using UnityEngine;


public class DirKeyState : KeyState
{
	public ReadOnlyReactiveProperty< Vector2Int >	Value;


	public Vector2Int Dir { get; }


	public DirKeyState( KeyCode keyCode, Vector2Int dir )
		: base( keyCode )
	{
		Dir			= dir;
	}


	public override void Init( ReadOnlyReactiveProperty<bool> isPressed )
	{
		base.Init( isPressed );

		Value		= IsPressed
						.Select( x => Dir * x.ToInt() )
						.ToReadOnlyReactiveProperty()
		;
	}
}

