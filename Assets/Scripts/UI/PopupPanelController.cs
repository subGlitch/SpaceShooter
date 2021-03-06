﻿

public class PopupPanelController
{
	PopupPanelView		_view;


	public PopupPanelController( PopupPanelView view )
	{
		_view		= view;

		view.OnMap		+= ToMap;
	}


	public void ToMap()
	{
		_view.Close();

		Refs.Instance.MapView.SetActive( true );
		LevelController.CloseLevel();
	}


	public void OpenPanel( bool isWin )
	=>
		_view.Open( isWin );
}

