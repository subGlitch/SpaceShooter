using UniRx;


public class MapView : UiViewBase
{
	public Subject< int > LevelPress		= new Subject< int >();


	public void OnLevel( int levelIndex )
	{
		SetActive( false );

		LevelPress.OnNext( levelIndex );
	}
}

