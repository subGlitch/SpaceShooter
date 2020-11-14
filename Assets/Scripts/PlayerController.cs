using UniRx;
using UnityEngine;


public class PlayerController
{
	PlayerView		_view;
	PlayerModel		_model;


	bool	_right;


	public PlayerController( PlayerModel model, PlayerView view )
	{
		_view		= view;
		_model		= model;

		model.OnPositionChange		+= ModelOnOnPositionChange;


		Observable.EveryUpdate()
			.Where( _ => Input.GetKeyDown( KeyCode.D ) || Input.GetKeyUp( KeyCode.D ) )
			.Select( _ => Input.GetKeyDown( KeyCode.D ) )
			.Subscribe( x => _right = x )
		;
		
		Observable.EveryUpdate()
			.Where( _ => _right )
			.Subscribe( _ => _model.Move( Vector2Int.right ) )
		;
	}


	void ModelOnOnPositionChange( Vector2 pos )
	{
		_view.transform.position		= pos;
	}
}

