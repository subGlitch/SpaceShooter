using UniRx;


public static class UiController
{
	public static void BindShipModel( ShipModel shipModel )
	{
		shipModel.Hull
			.Subscribe( x => Refs.Instance.HudView.SetHull( x ) );
	}


	public static void OpenPopupPanel()
	{
		Refs.Instance.PopupPanelView.gameObject.SetActive( true );
	}
}

