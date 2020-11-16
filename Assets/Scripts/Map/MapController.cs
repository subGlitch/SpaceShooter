using UniRx;


public class MapController
{
	public MapController( MapModel model, MapView view )
	{
		int maxCompleted		= model.MaxCompleted;

		for (int i = 0; i < view.Locations.Count; i ++)
		{
			MapLocationState state		=
											i <= maxCompleted ? MapLocationState.Completed :
											i == maxCompleted + 1 ? MapLocationState.Available :
											MapLocationState.Locked
			;

			view.Locations[ i ].SetState( state );
		}


		view.LocationSelect.Subscribe( levelIndex =>
		{
			view.SetActive( false );
			LevelController.Instance.StartLevel( levelIndex );
		});
	}
}

