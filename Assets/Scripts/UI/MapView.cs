using UnityEngine;


public class MapView : MonoBehaviour
{
	public void OnLevel( int levelIndex )
	{
		LevelController.Instance.StartLevel();
		
		SetActive( false );
	}


	public void SetActive( bool isActive )
	{
		gameObject.SetActive( isActive );
	}
}

