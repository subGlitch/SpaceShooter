using UniRx;
using UnityEngine;


public class PlayerModel
{
	public readonly ReactiveProperty< float >	Speed;


	Vector2		_position;


	public PlayerModel( float speed )
	{
		Speed		= new ReactiveProperty< float >( speed );
	}


	public void RefreshPosition( Vector2 pos )
	{
		_position		= pos;
	}
}

