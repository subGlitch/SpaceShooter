using UniRx;
using UnityEngine;


public class ShipController : ADestroyableController
{
#region Keyboard Controls

	DirKeyState		_keyRight		= new DirKeyState( KeyCode.D,	Vector2Int.right	);
	DirKeyState		_keyLeft		= new DirKeyState( KeyCode.A,	Vector2Int.left		);
	DirKeyState		_keyUp			= new DirKeyState( KeyCode.W,	Vector2Int.up		);
	DirKeyState		_keyDown		= new DirKeyState( KeyCode.S,	Vector2Int.down		);

#endregion


	ShipView	_view;


	public ShipController( ShipModel model, ShipView view )
	{
		_view		= view;


		// Init Player Controls
		BindDirKeys();
		Observable
			.EveryUpdate()
			.Where( _ => Input.GetMouseButtonDown( 0 ) && !Utils.IsPointerOverGameObject() )
			.Subscribe( _ => model.Fire() )
			.AddTo( view );


		// Bind View
		view.Direction		= MergeDirKeys();
		view.Speed			= model.Speed.ToReadOnlyReactiveProperty();

		// Bind Model
		{
			model.Position			= Observable.EveryFixedUpdate()
											.Select( _ => view.Position )
											.ToReadOnlyReactiveProperty();

			view.TriggerEnterEvents
					.Subscribe( _ => model.TakeDamage() );

			model.OnDestroyed		+= Destroy;
		}
	}


	public override void DestroySilently()
	=>
		GameObject.Destroy( _view.gameObject );

	
	void BindDirKeys()
	{
		BindKey( _keyRight	);
		BindKey( _keyLeft	);
		BindKey( _keyUp		);
		BindKey( _keyDown	);
	}


	ReadOnlyReactiveProperty< Vector2Int > MergeDirKeys()
	{
		return
			Observable
				.Merge(
					_keyRight	.IsPressed,
					_keyLeft	.IsPressed,
					_keyUp		.IsPressed,
					_keyDown	.IsPressed
				)
				.Select( x =>
					(Vector2Int)
					_keyRight	+
					_keyLeft	+
					_keyUp		+
					_keyDown
				)
				.ToReadOnlyReactiveProperty()
		;
	}


	void BindKey( KeyState keyState )
	{
		KeyCode keyCode		= keyState.KeyCode;

		var isPressed		= Observable.EveryUpdate()
								.Where( _ =>
									Input.GetKeyDown	( keyCode ) ||
									Input.GetKeyUp		( keyCode )
								)
								.Select( _ =>
									Input.GetKey		( keyCode )
								)
								.ToReadOnlyReactiveProperty()
		;

		keyState.Init( isPressed );
	}
}

