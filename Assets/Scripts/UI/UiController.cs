using UniRx;


public static class UiController
{
	public static void Init()
	{
		Refs.Instance.PopupPanelView.OnRestart		+= OnLevelRestart;
	}


	static void OnLevelRestart()
	{
		LevelController.Instance.RestartLevel();

		PopupPanelSetActive( false );
	}


	public static void BindShipModel( ShipModel shipModel )
	{
		shipModel.Hull
			.Subscribe( x => Refs.Instance.HudView.SetHull( x ) );
	}


	public static void OpenPopupPanel()
	{
		PopupPanelSetActive( true );
	}


	static void PopupPanelSetActive( bool active )
	{
		Refs.Instance.PopupPanelView.gameObject.SetActive( active );
	}
}

