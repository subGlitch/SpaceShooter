using UniRx;
using UnityEngine;


public static class UiControllers
{
	public static HudController				HudController;
	public static PopupPanelController		PopupPanelController;


	public static void Init()
	{
		HudController				= new HudController( Refs.Instance.HudView );
		PopupPanelController		= new PopupPanelController( Refs.Instance.PopupPanelView );

		// [Escape] - Quit
		Observable.EveryUpdate()
			.Where( _ => Input.GetKeyDown( KeyCode.Escape ) )
			.Subscribe( _ => Application.Quit() )
		;
	}
}

