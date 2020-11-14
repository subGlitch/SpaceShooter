using UnityEngine;


public class Test : MonoBehaviour
{
	public PlayerView		_playerView;


	void Start()
	{
		const float speed		= 1;

	    PlayerModel model				= new PlayerModel( speed );
		PlayerController controller		= new PlayerController( model, _playerView );
	}
}

