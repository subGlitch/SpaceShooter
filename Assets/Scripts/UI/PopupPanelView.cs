using System;
using UnityEngine;


public class PopupPanelView : MonoBehaviour
{
	public event Action		OnRestart;


	public void Restart()
	{
		OnRestart?.Invoke();
	}
}

