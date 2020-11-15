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

#endregion


	public PlayerController( PlayerModel model, PlayerView view )
	{
		_view		= view;
		_model		= model;

		// Init Player Controls
		BindKeys();

		// Bind View
		_view.Direction		= MergeKeys();
		_view.Speed			= _model.Speed.ToReadOnlyReactiveProperty();

		// Bind Model
		_model.Position		= Observable.EveryFixedUpdate()
								.Select( _ => _view.Position )
								.ToReadOnlyReactiveProperty();
	}

	
	void BindKeys()
	{
		BindKey( _keyRight	);
		BindKey( _keyLeft	);
		BindKey( _keyUp		);
		BindKey( _keyDown	);
	}


	ReadOnlyReactiveProperty< Vector2Int > MergeKeys()
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

