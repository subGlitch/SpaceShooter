using UniRx;


public class HudController
{
	HudView		_view;


	public HudController( HudView view )
	{
		_view		= view;
	}


	public void BindShipModel( ShipModel shipModel )
	{
		shipModel.Hull
			.Subscribe( x => _view.SetHull( x ) );
	}
}

