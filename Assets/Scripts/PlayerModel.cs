using UnityEngine;


public class PlayerModel
{
	public delegate void PositionEvent( Vector2 pos );
	public event PositionEvent OnPositionChange;


	Vector2		_position;
	float		_speed;


	public PlayerModel( float speed )
	{
		_speed		= speed;
	}


	public void Move( Vector2Int dir )
	{
		_position		+= (Vector2)dir * _speed;

		OnPositionChange?.Invoke( _position );
	}
}

