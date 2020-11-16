using UnityEditor;
using UnityEngine;


public class GameController : MonoBehaviour
{
	void Start()
	{
		TechSettings();
		GenerateRandomParameters();

		UiControllers.Init();

		Refs.Instance.MapView.SetActive( true );
	}


	void GenerateRandomParameters()
	{
		Random.InitState( 1 );

		Settings settings		= Refs.Instance.Settings;

		foreach (LevelConfig levelConfig in settings.Levels)
		{
			levelConfig.Seed	= Random.Range( int.MinValue, int.MaxValue );
		}

		#if UNITY_EDITOR
			// EditorUtility.SetDirty( settings );
		#endif
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

