using UnityEngine;


public class Test : MonoBehaviour
{
	public PlayerView		_playerView;
	public PlayerInputView	_playerInputView;


	void Start()
	{
		const float speed		= 1;

	    PlayerModel model				= new PlayerModel( speed );
		PlayerController controller		= new PlayerController( model, _playerView, _playerInputView );
	}
}

