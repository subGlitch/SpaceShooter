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
		view.TriggerEnterEvents.Subscribe( col =>
		{
			switch (col.gameObject.layer)
			{
				case Layers.AsteroidsTriggers:
					LevelController.Instance.ChangeAsteroidsOnScreenCount( 1 );
					break;

				default:
					Destroy();
					break;
			}
		});

		view.TriggerExitEvents.Subscribe( col =>
		{
			switch (col.gameObject.layer)
			{
				case Layers.AsteroidsTriggers:
					LevelController.Instance.ChangeAsteroidsOnScreenCount( -1 );
					break;
			}
		});
	}


	public override void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );
}

