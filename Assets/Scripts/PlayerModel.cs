using UniRx;
using UnityEngine;


public class PlayerModel
{
	public readonly ReactiveProperty< Vector2 >		position		= new ReactiveProperty< Vector2 >();


	float	_speed;


	public PlayerModel( float speed )
	{
		_speed		= speed;
	}


	public void Move( Vector2Int dir )
	{
		Vector2 raw				= position.Value + (Vector2)dir * _speed * Time.deltaTime;
		Vector2 clamped01		= Rect.PointToNormalized( Boundaries.Rect, raw );
		Vector2 clamped			= Rect.NormalizedToPoint( Boundaries.Rect, clamped01 );

		position.Value			= clamped;
	}
}

