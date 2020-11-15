

public class PopupPanelController
{
	PopupPanelView		_view;


	public PopupPanelController( PopupPanelView view )
	{
		_view		= view;

		view.OnRestart		+= OnLevelRestart;
	}


	void OnLevelRestart()
	{
		LevelController.Instance.RestartLevel();

		PanelSetActive( false );
	}


	public void OpenPanel()
	{
		PanelSetActive( true );
	}


	void PanelSetActive( bool active )
	{
		_view.gameObject.SetActive( active );
	}
}

