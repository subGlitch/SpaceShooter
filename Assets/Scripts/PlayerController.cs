using System;
using UniRx;
using UnityEngine;


public class PlayerController
{
	PlayerView		_view;
	PlayerModel		_model;


	// Keyboard Controls
	DirKeyState		_keyRight		= new DirKeyState( KeyCode.D,	Vector2Int.right	);
	DirKeyState		_keyLeft		= new DirKeyState( KeyCode.A,	Vector2Int.left		);
	DirKeyState		_keyUp			= new DirKeyState( KeyCode.W,	Vector2Int.up		);
	DirKeyState		_keyDown		= new DirKeyState( KeyCode.S,	Vector2Int.down		);
	Vector2Int		Dir				=> (Vector2Int) _keyRight + _keyLeft + _keyUp + _keyDown;


	public PlayerController( PlayerModel model, PlayerView view )
	{
		_view		= view;
		_model		= model;

		// Player Control
		BindKeys();
		Observable.EveryUpdate()
			.Where( _ => Dir != Vector2Int.zero )
			.Subscribe( _ => _model.Move( Dir ) )
		;

		// View Refresh
		model.position
			.Subscribe( p => _view.transform.position = p )
			.AddTo( _view )
		;
	}

	
	void BindKeys()
	{
		BindKey( _keyRight	);
		BindKey( _keyLeft	);
		BindKey( _keyUp		);
		BindKey( _keyDown	);
	}


	void BindKey( KeyState keyState )
	=>
		BindKey( keyState.KeyCode, keyState.Set );


	void BindKey( KeyCode keyCode, Action< bool > action )
	=>
		Observable.EveryUpdate()
			.Where( _ =>
				Input.GetKeyDown	( keyCode ) ||
				Input.GetKeyUp		( keyCode )
			)
			.Select( _ =>
				Input.GetKeyDown	( keyCode )
			)
			.Subscribe( action )
		;
}

