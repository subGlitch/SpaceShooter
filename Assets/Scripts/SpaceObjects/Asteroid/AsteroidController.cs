﻿using UniRx;
using UnityEngine;


public class AsteroidController : ADestroyable
{
	AsteroidView	_view;


	public AsteroidController( AsteroidView view )
	{
		_view		= view;

		view.TriggerEvents.Subscribe( _ => Destroy() );
	}


	public override void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );
}

