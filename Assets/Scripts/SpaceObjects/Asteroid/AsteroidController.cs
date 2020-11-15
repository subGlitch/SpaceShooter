﻿using UniRx;
using UnityEngine;


public class AsteroidController
{
	public delegate void AsteroidControllerEvent( AsteroidController controller );
	public event AsteroidControllerEvent	OnDestroy;


	AsteroidView	_view;


	public AsteroidController( AsteroidView view )
	{
		_view		= view;

		view.Collisions.Subscribe( _ => Destroy() );
	}


	void Destroy()
	{
		DestroySilently();
		OnDestroy?.Invoke( this );
	}


	public void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );
}
