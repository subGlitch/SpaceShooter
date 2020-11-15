using UniRx;
using UnityEngine;


public class KeyState
{
	public KeyCode KeyCode { get; }


	public readonly ReactiveProperty< bool > IsPressed		= new ReactiveProperty< bool >();


	public int ToInt	=> IsPressed.Value.ToInt();


	public KeyState( KeyCode keyCode )
	{
		KeyCode		= keyCode;
	}


	public void Set( bool isPressed )
	{
		IsPressed.Value		= isPressed;
	}
}

