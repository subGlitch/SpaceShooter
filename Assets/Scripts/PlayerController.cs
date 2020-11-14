using System;
using UniRx;
using UnityEngine;


public class PlayerController
{
	PlayerView		_view;
	PlayerModel		_model;


	bool	_right;
	bool	_left;
	bool	_up;
	bool	_down;


	Vector2Int Dir		=> new Vector2Int(
											_right.ToInt() - _left.ToInt(),
											_up.ToInt() - _down.ToInt()
	);


	public PlayerController( PlayerModel model, PlayerView view )
	{
		_view		= view;
		_model		= model;

		BindKey( KeyCode.A, x => _left = x );
		BindKey( KeyCode.D, x => _right = x );
		BindKey( KeyCode.S, x => _down = x );
		BindKey( KeyCode.W, x => _up = x );
		
		Observable.EveryUpdate()
			.Where( _ => Dir != Vector2Int.zero )
			.Subscribe( _ => _model.Move( Dir ) )
		;

		model.OnPositionChange		+= OnPositionChange;
	}


	void OnPositionChange( Vector2 pos )
	{
		_view.transform.position		= pos;
	}


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

