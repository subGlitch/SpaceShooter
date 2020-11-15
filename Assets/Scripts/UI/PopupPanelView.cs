using UnityEngine;


public class PopupPanelView : MonoBehaviour
{
	public void OnRestart()
	{
		LevelController.Instance.RestartLevel();

		gameObject.SetActive( false );
	}
}

