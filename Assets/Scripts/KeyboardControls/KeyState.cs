using UnityEngine;


public class KeyState
{
	public KeyCode KeyCode { get; }
	public bool IsPressed { get; private set; }


	public int ToInt	=> IsPressed.ToInt();


	public KeyState( KeyCode keyCode )
	{
		KeyCode		= keyCode;
	}


	public void Set( bool isPressed )
	{
		IsPressed		= isPressed;
	}
}

