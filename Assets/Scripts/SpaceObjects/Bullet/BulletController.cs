using UniRx;
using UnityEngine;


public class BulletController : ADestroyable
{
	BulletView	_view;


	public BulletController( BulletView view )
	{
		_view		= view;

		view.Collisions.Subscribe( _ => Destroy() );
	}


	public override void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );

}

