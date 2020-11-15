using UniRx;
using UnityEngine;


public class KeyState
{
	public ReadOnlyReactiveProperty< bool > IsPressed;


	public KeyCode KeyCode { get; }


	public KeyState( KeyCode keyCode )
	{
		KeyCode			= keyCode;
	}


	public void Init( ReadOnlyReactiveProperty< bool > isPressed )
	{
		IsPressed		= isPressed;
	}
}

