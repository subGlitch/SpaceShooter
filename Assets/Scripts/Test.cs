using UnityEngine;


public class Test : MonoBehaviour
{
	public PlayerView		_playerView;


	void Start()
	{
		const float speed		= 5;

		_playerView.Init( speed );

	    PlayerModel model				= new PlayerModel();
		PlayerController controller		= new PlayerController( model, _playerView );
	}
}

