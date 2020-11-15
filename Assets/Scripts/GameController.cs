using UniRx;
using UnityEngine;


public class GameController : MonoBehaviour
{
	public ShipView				_shipView;
	public ShipTriggerView		_shipTriggerView;
	public HudView				_hudView;


	void Start()
	{
		TechSettings();

		// [Escape] - Quit
		Observable.EveryUpdate()
			.Where( _ => Input.GetKeyDown( KeyCode.Escape ) )
			.Subscribe( _ => Application.Quit() )
		;

		const float speed				= 5;

	    ShipModel model				= new ShipModel( speed );
		ShipController controller		= new ShipController( model, _shipView, _shipTriggerView );

		model.Hull.Subscribe( x => _hudView.SetHull( x ) );
	}


	void TechSettings()
	{
		#if UNITY_EDITOR
		QualitySettings.vSyncCount				= 1;			// for FPS cap in Unity Editor
		Application.targetFrameRate				= 60;			// for FPS cap in Unity Editor
		#else
		if (Application.isMobilePlatform)
			Application.targetFrameRate			= 60;
		else
			Application.targetFrameRate			= -1;
		#endif

		QualitySettings.maxQueuedFrames			= 0;
		Screen.sleepTimeout						= SleepTimeout.NeverSleep;
	}
}

