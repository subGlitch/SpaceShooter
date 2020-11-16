

public class MapView : UiViewBase
{
	public void OnLevel( int levelIndex )
	{
		LevelController.Instance.StartLevel();
		
		SetActive( false );
	}
}

