using UnityEngine;
using UnityEngine.UI;


public class HudView : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text		_hull;

#pragma warning restore 0649


	public void SetHull( int hull )
	{
		_hull.text		= $"Hull: { hull }";
	}


	public void OnOK()
	{
		LevelController.Instance.RestartLevel();

		gameObject.SetActive( false );
	}
}

