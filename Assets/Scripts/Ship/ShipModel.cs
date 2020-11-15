using UniRx;
using UnityEngine;


public class ShipModel
{
	public ReactiveProperty< int >					Hull		{ get; }
	public ReactiveProperty< float >				Speed		{ get; }

	public ReadOnlyReactiveProperty< Vector2 >		Position;


	public ShipModel( float speed )
	{
		Hull		= new ReactiveProperty< int >( 3 );
		Speed		= new ReactiveProperty< float >( speed );
	}


	public void TakeDamage()
	{
		Hull.Value --;
	}
}

