using UniRx;
using UnityEngine;


public class BulletController : ADestroyable
{
	BulletView	_view;


	public BulletController( BulletModel model, BulletView view )
	{
		_view		= view;

		model.Velocity.Subscribe( v => _view.SetVelocity( v ) );

		view.Collisions.Subscribe( _ => Destroy() );
	}


	public override void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );

}

