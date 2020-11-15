using UniRx;
using UnityEngine;


public class PlayerModel
{
	public readonly ReactiveProperty< Vector2Int >	direction		= new ReactiveProperty< Vector2Int >();


	Vector2		_position;


	public void RefreshPosition( Vector2 pos )
	{
		_position		= pos;
	}
}

