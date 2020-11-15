using UniRx;
using UnityEngine;


public class PlayerModel
{
	public ReactiveProperty< int >					Hull		{ get; }
	public ReactiveProperty< float >				Speed		{ get; }

	public ReadOnlyReactiveProperty< Vector2 >		Position;


	public PlayerModel( float speed )
	{
		Hull		= new ReactiveProperty< int >( 3 );
		Speed		= new ReactiveProperty< float >( speed );
	}
}

