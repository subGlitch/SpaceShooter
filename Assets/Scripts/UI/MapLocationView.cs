using UniRx;
using UnityEngine;
using UnityEngine.UI;


public enum MapLocationState
{
	None,

	Locked,
	Available,
	Completed,
}


public class MapLocationView : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button		_button;
	[SerializeField] Image		_image;
	[SerializeField] Text		_text;

#pragma warning restore 0649


	public Subject< int >	PressEvents		= new Subject< int >();


	void Start()
	{
		_button.onClick.AddListener( () => PressEvents.OnNext( -1 ) );
	}


	public void SetLevelNum( int levelNum )
	{
		_text.text		= levelNum.ToString();
	}


	public void SetState( MapLocationState state )
	{
		_button.interactable		= state >= MapLocationState.Available;

		_image.color				= StateColor( state );
	}


	Color StateColor( MapLocationState state )
	{
		switch (state)
		{
			case MapLocationState.Locked:		return Color.red;
			case MapLocationState.Available:	return Color.white;
			case MapLocationState.Completed:	return Color.green;

			default:
				return Color.magenta;
		}
	}
}

