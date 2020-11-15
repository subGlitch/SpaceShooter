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

	ReadOnlyReactiveProperty< Vector2Int >	_direction;

#endregion


	public PlayerController( PlayerModel model, PlayerView view )
	{
		_view		= view;
		_model		= model;

		// Init Player Controls
		BindKeys();

		// Refresh View's direction 
		_direction
			.Subscribe( x => _view.SetDirection( x ) );

		// Refresh Model's position
		Observable.EveryFixedUpdate()
			.Subscribe( _ => _model.RefreshPosition( _view.transform.position ) );
	}

	
	void BindKeys()
	{
		BindKey( _keyRight	);
		BindKey( _keyLeft	);
		BindKey( _keyUp		);
		BindKey( _keyDown	);

		_direction		= Observable
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

