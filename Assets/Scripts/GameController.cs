using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
	const string SaveFileName		= "save.dat";
	string SaveFilePath				=> SavingSystem.DefaultPath( SaveFileName );
	

	MapModel	_mapModel;


	void Start()
	{
		TechSettings();
		GenerateRandomParameters();

		try
		{
			LoadGame();
		}
		catch
		{
			CreateNewGame();
		}

		UiControllers.Init( _mapModel );

		Refs.Instance.MapView.SetActive( true );
	}


	void OnApplicationQuit()
	{
		SaveGame();	
	}

	/*
    void OnApplicationFocus( bool hasFocus )
	{
		if (
				!hasFocus &&
				Time.frameCount > 0				// Strange cases on some devices
			)
		{
			SaveGame();	
		}
	}
	*/

	void OnApplicationPause( bool pauseStatus )
	{
		if (
				pauseStatus &&
				Time.frameCount > 0				// Strange cases on some devices
			)
		{
			SaveGame();	
		}
	}


	void CreateNewGame()		=> _mapModel		= new MapModel();
	void LoadGame()				=> _mapModel		= SavingSystem.Deserialize< MapModel >( SaveFilePath );
	void SaveGame()				=> SavingSystem.Serialize( SaveFilePath, _mapModel );


	void GenerateRandomParameters()
	{
		Random.InitState( 1 );

		Settings settings		= Refs.Instance.Settings;

		foreach (LevelConfig levelConfig in settings.Levels)
		{
			levelConfig.Seed	= Random.Range( int.MinValue, int.MaxValue );
		}

		#if UNITY_EDITOR
			EditorUtility.SetDirty( settings );
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

