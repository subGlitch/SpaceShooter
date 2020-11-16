using UniRx;


public class MapController
{
	public MapController( MapView view )
	{
		view.LocationSelect.Subscribe( levelIndex =>
		{
			view.SetActive( false );
			LevelController.Instance.StartLevel( levelIndex );
		});
	}
}

