using UniRx;
using UnityEngine;


public class Test : MonoBehaviour
{
	public PlayerView		_playerView;


	void Start()
	{
		TechSettings();

		// [Escape] - Quit
		Observable.EveryUpdate()
			.Where( _ => Input.GetKeyDown( KeyCode.Escape ) )
			.Subscribe( _ => Application.Quit() )
		;

		const float speed				= 5;

	    PlayerModel model				= new PlayerModel( speed );
		PlayerController controller		= new PlayerController( model, _playerView );
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

