using UnityEngine;


public class PlayerController
{
	PlayerView		_view;
	PlayerModel		_model;


	public PlayerController( PlayerModel model, PlayerView view, PlayerInputView input )
	{
		_view		= view;
		_model		= model;

		input.OnMove				+= InputOnOnMove;
		model.OnPositionChange		+= ModelOnOnPositionChange;
	}


	void InputOnOnMove( Vector2Int dir )
	{
		_model.Move( dir );
	}


	void ModelOnOnPositionChange( Vector2 pos )
	{
		_view.transform.position		= pos;
	}
}

