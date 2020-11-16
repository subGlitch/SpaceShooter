using UniRx;
using UnityEngine;


public class AsteroidController : ADestroyableController
{
	AsteroidView	_view;


	public AsteroidController( AsteroidModel model, AsteroidView view )
	{
		_view		= view;

		// Bind Model
		model.Velocity.Subscribe( v => _view.SetVelocity( v ) );

		// Bind View
		view.TriggerEvents.Subscribe( _ => Destroy() );
	}


	public override void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );
}

