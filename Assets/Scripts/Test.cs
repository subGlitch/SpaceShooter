using UnityEngine;


public class Test : MonoBehaviour
{
	public PlayerView		_playerView;


	void Start()
	{
		const float speed		= 5;

	    PlayerModel model				= new PlayerModel( speed );
		PlayerController controller		= new PlayerController( model, _playerView );
	}
}

