using UniRx;
using UnityEngine;


public class MapModel
{
	public ReactiveProperty< int > MaxCompleted		= new ReactiveProperty< int >( -1 );


	public void OnLevelCompleted( int levelIndex )
	{
		MaxCompleted.Value		= Mathf.Max( MaxCompleted.Value, levelIndex );
	}
}

