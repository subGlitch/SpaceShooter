using UniRx;
using UnityEngine;


public class ShipController
{
#region Keyboard Controls

	DirKeyState		_keyRight		= new DirKeyState( KeyCode.D,	Vector2Int.right	);
	DirKeyState		_keyLeft		= new DirKeyState( KeyCode.A,	Vector2Int.left		);
	DirKeyState		_keyUp			= new DirKeyState( KeyCode.W,	Vector2Int.up		);
	DirKeyState		_keyDown		= new DirKeyState( KeyCode.S,	Vector2Int.down		);

#endregion


	public ShipController( ShipModel model, ShipView view )
	{
		// Init Player Controls
		BindKeys();


		// Bind View
		view.Direction		= MergeKeys();
		view.Speed			= model.Speed
									.ToReadOnlyReactiveProperty();

		// Bind Model
		{
			model.Position			= Observable.EveryFixedUpdate()
											.Select( _ => view.Position )
											.ToReadOnlyReactiveProperty();

			view.Collisions
					.Subscribe( _ => model.TakeDamage() );

			model.OnDestroyed		+= () => GameObject.Destroy( view.gameObject );
		}
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

