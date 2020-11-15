

public class PopupPanelController
{
	PopupPanelView		_view;


	public PopupPanelController( PopupPanelView view )
	{
		_view		= view;

		view.OnRestart		+= RestartLevel;
	}


	public void RestartLevel()
	{
		LevelController.Instance.RestartLevel();

		PanelSetActive( false );
	}


	public void OpenPanel()
	=>
		PanelSetActive( true );


	void PanelSetActive( bool active )
	=>
		_view.gameObject.SetActive( active );
}

