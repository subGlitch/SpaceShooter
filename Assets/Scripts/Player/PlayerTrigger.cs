using UniRx;
using UniRx.Triggers;
using UnityEngine;


public class PlayerTrigger : MonoBehaviour
{
	void Start()
	{
		gameObject
			.OnTriggerEnter2DAsObservable()
			.Subscribe( _ => Debug.Log( "Trigger!" ) );
	}
}
