using UniRx;
using UnityEngine;


public class PlayerController
{
	PlayerView		_view;
	PlayerModel		_model;


#region Keyboard Controls

	DirKeyState		_keyRight		= new DirKeyState( KeyCode.D,	Vector2Int.right	);
	DirKeyState		_keyLeft		= new DirKeyState( KeyCode.A,	Vector2Int.left		);
	DirKeyState		_keyUp			= new DirKeyState( KeyCode.W,	Vector2Int.up		);
	DirKeyState		_keyDown		= new DirKeyState( KeyCode.S,	Vector2Int.down		);

	ReadOnlyReactiveProperty< Vector2Int >	Dir;

#endregion


	public PlayerController( PlayerModel model, PlayerView view )
	{
		_view		= view;
		_model		= model;

		// Player Control
		BindKeys();
		Observable.EveryUpdate()
			.Subscribe( _ => _view.SetVelocity( Dir.Value ) )
		;
		Observable.EveryFixedUpdate()
			.Subscribe( _ => _view.SetVelocity( Dir.Value ) )
		;

		// Refresh Model's position
		Observable.EveryFixedUpdate()
			.Subscribe( _ => _model.RefreshPosition( _view.transform.position ) )
		;
	}

	
	void BindKeys()
	{
		BindKey( _keyRight	);
		BindKey( _keyLeft	);
		BindKey( _keyUp		);
		BindKey( _keyDown	);

		Dir		= Observable.Merge(
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
		KeyCode keyCode			= keyState.KeyCode;

		var isPressed			= Observable.EveryUpdate()
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

