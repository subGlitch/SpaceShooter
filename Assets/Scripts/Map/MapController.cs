using UniRx;


public class MapController
{
	MapView		_view;
	MapModel	_model;


	public MapController( MapModel model, MapView view )
	{
		_view		= view;
		_model		= model;

		// Subscribe to Location Select
		view.LocationSelect.Subscribe( levelIndex =>
		{
			view.SetActive( false );
			LevelController.StartLevel( levelIndex );
		});

		// Subscribe to LevelCompleted events
		LevelController.LevelCompletedEvents.Subscribe( model.OnLevelCompleted );

		// Refresh Map when MaxCompleted changes
		model.MaxCompleted.Subscribe( _ => RefreshView() );
	}


	void RefreshView()
	{
		int maxCompleted		= _model.MaxCompleted.Value;

		for (int i = 0; i < _view.Locations.Count; i ++)
		{
			MapLocationState state		=
											i <= maxCompleted		?	MapLocationState.Completed :
											i == maxCompleted + 1	?	MapLocationState.Available :
																		MapLocationState.Locked
			;

			_view.Locations[ i ].SetState( state );
		}
	}
}

