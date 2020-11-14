using System;
using UniRx;
using UnityEngine;


public class PlayerController
{
	PlayerView		_view;
	PlayerModel		_model;

	KeyState	_keyRight		= new KeyState( KeyCode.D );
	KeyState	_keyLeft		= new KeyState( KeyCode.A );
	KeyState	_keyUp			= new KeyState( KeyCode.W );
	KeyState	_keyDown		= new KeyState( KeyCode.S );

	Vector2Int Dir		=> new Vector2Int(
											_keyRight.ToInt - _keyLeft.ToInt,
											_keyUp.ToInt - _keyDown.ToInt
	);


	public PlayerController( PlayerModel model, PlayerView view )
	{
		_view		= view;
		_model		= model;

		BindKey( _keyRight	);
		BindKey( _keyLeft	);
		BindKey( _keyUp		);
		BindKey( _keyDown	);
		
		Observable.EveryUpdate()
			.Where( _ => Dir != Vector2Int.zero )
			.Subscribe( _ => _model.Move( Dir ) )
		;

		model.OnPositionChange		+= OnPositionChange;
	}


	void OnPositionChange( Vector2 pos )
	=>
		_view.transform.position		= pos;


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

