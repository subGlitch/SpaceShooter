using System;
using UniRx;
using UnityEngine;


public static class UiControllers
{
	public static MapController				MapController;
	public static HudController				HudController;
	public static PopupPanelController		PopupPanelController;


	public static void Init()
	{
		MapController				= new MapController( Refs.Instance.MapView );
		HudController				= new HudController( Refs.Instance.HudView );
		PopupPanelController		= new PopupPanelController( Refs.Instance.PopupPanelView );

		// Special Keys
		BindKey( KeyCode.Escape, Application.Quit );		// [Escape] - Quit

	}


	static void BindKey( KeyCode keyCode, Action action )
	{
		Observable.EveryUpdate()
			.Where( _ => Input.GetKeyDown( keyCode ) )
			.Subscribe( _ => action() )
		;
	}
}

