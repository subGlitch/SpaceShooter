using UniRx;


public class HudController
{
	HudView		_view;


	public HudController( HudView view )
	{
		_view		= view;

		LevelController.Instance.TimerEndTime.Subscribe( t => _view.SetTimerEndTime( t ) );
	}


	public void BindShipModel( ShipModel shipModel )
	{
		shipModel.Hull
			.Subscribe( x => _view.SetHull( x ) );
	}
}

