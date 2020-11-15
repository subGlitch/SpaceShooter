using UniRx;
using UnityEngine;


public class PlayerView : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Rigidbody2D		_rb;

#pragma warning restore 0649


	public ReadOnlyReactiveProperty< Vector2Int >		Direction;
	public ReadOnlyReactiveProperty< float >			Speed;


	float	_speed;

	public void Init( float speed )
	{
		_speed		= speed;
	}


	void FixedUpdate()
	{
		// _rb.velocity		= (Vector2)Direction.Value * Speed.Value;
		_rb.velocity		= (Vector2)Direction.Value * _speed;
	}
}

