using UniRx;
using UnityEngine;


public class AsteroidController : ADestroyableController
{
	static int	_onScreenCounter;


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
					Debug.Log( $"_onScreenCounter: { ++ _onScreenCounter }");
					break;

				default:
					Destroy();
					break;
			}
		});
	}


	public override void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );
}

