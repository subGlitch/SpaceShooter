

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

		_view.Close();
	}


	public void OpenPanel( bool isWin )
	=>
		_view.Open( isWin );
}

