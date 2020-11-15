using UniRx;
using UnityEngine;


public class KeyState
{
	public KeyCode KeyCode { get; }


	public ReadOnlyReactiveProperty< bool > IsPressed;


	public int ToInt	=> IsPressed.Value.ToInt();


	public KeyState( KeyCode keyCode )
	{
		KeyCode		= keyCode;
	}
}

