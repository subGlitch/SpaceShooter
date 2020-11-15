using UnityEngine;


public class PlayerView : MonoBehaviour
{
	float	_speed;


	public void Init( float speed )
	{
		_speed		= speed;
	}


	public void SetVelocity( Vector2Int dir )
	{
		GetComponent< Rigidbody2D >().velocity		= (Vector2)dir * _speed;
	}
}

