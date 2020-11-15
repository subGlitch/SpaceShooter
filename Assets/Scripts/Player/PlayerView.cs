using UnityEngine;


public class PlayerView : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Rigidbody2D		_rb;

#pragma warning restore 0649


	float			_speed;
	Vector2Int		_dir;


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

