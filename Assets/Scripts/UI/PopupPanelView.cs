using System;
using UnityEngine;
using UnityEngine.UI;


public class PopupPanelView : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text	_message;

#pragma warning restore 0649


	public event Action		OnRestart;


	public void Restart()
	=>
		OnRestart?.Invoke();


	public void Open( bool isWin )
	{
		gameObject.SetActive( true );

		_message.text		=
								isWin					?
								"Level complete!"		:
								"Your ship destroyed!"
		;
	}


	public void Close()
	{
		gameObject.SetActive( false );
	}
}

