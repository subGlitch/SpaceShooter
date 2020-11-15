using UniRx;
using UnityEngine;


public static class UiControllers
{
	public static HudController		HudController;


	public static void Init()
	{
		HudController		= new HudController( Refs.Instance.HudView );

		Refs.Instance.PopupPanelView.OnRestart		+= OnLevelRestart;

		// [Escape] - Quit
		Observable.EveryUpdate()
			.Where( _ => Input.GetKeyDown( KeyCode.Escape ) )
			.Subscribe( _ => Application.Quit() )
		;
	}


	static void OnLevelRestart()
	{
		LevelController.Instance.RestartLevel();

		PopupPanelSetActive( false );
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

