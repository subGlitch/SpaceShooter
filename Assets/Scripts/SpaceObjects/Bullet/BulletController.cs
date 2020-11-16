using UniRx;
using UnityEngine;


public class BulletController : ADestroyable
{
	BulletView	_view;


	public BulletController( BulletModel model, BulletView view )
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

