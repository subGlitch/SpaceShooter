using UniRx;
using UniRx.Triggers;
using UnityEngine;


public class AsteroidView : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Rigidbody2D		_rb;

#pragma warning restore 0649


	public void Init( Vector2 velocity )
	{
		_rb.velocity	 = velocity;
	}


	void Start()
	{
		gameObject
			.OnTriggerEnter2DAsObservable()
			.Subscribe( _ => Debug.Log( "Coll" ))
		;
	}
}

