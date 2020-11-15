using UniRx;


public static class HudController
{
	public static void BindShipModel( ShipModel shipModel )
	{
		shipModel.Hull
			.Subscribe( x => Refs.Instance.HudView.SetHull( x ) );
	}
}

