using UnityEngine;


[RequireComponent( typeof(Rigidbody2D) )]
public class PlayerView : MonoBehaviour
{
	float			_speed;
	Vector2Int		_dir;

	Rigidbody2D		_rb;


	void Awake()
	{
		_rb			= GetComponent< Rigidbody2D >();
	}


	public void Init( float speed )
	{
		_speed		= speed;
	}


	public void SetDirection( Vector2Int dir )
	{
		_dir		= dir;
	}

	void FixedUpdate()
	{
		_rb.velocity		= (Vector2)_dir * _speed;
	}
}

