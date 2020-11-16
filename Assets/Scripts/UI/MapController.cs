using UniRx;


public class MapController
{
	public MapController( MapView view )
	{
		view.LevelPress
			.Subscribe( levelIndex => LevelController.Instance.StartLevel( levelIndex ) );
	}
}

