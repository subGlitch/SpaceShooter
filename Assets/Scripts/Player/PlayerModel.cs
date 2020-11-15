using UniRx;
using UnityEngine;


public class PlayerModel
{
	public ReactiveProperty< float >			Speed		{ get; }
	public ReadOnlyReactiveProperty< Vector2 >	Position;


	public PlayerModel( float speed )
	{
		Speed		= new ReactiveProperty< float >( speed );
	}
}

