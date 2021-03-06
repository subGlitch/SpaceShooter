﻿using UniRx;


public class HudController
{
	HudView		_view;


	public HudController( HudView view )
	{
		_view		= view;

		LevelController.TimerEndTime.Subscribe( t => _view.SetTimerEndTime( t ) );
	}


	public void BindShipModel( ShipModel shipModel )
	{
		shipModel.Hull
			.Subscribe( x => _view.SetHull( x ) );
	}


	public void SetActive( bool isActive )
	{
		_view.SetActive( isActive );
	}
}

