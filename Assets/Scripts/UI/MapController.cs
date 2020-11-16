using UniRx;


public class MapController
{
	public MapController( MapView view )
	{
		view.LevelPress
			.Subscribe( _ => LevelController.Instance.StartLevel() );
	}
}

