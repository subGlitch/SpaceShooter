using UniRx.Triggers;
using UnityEngine;


public class TriggerView : MonoBehaviour
{
	public System.IObservable< Collider2D >		TriggerEnterEvents;


	void Awake()
	{
		TriggerEnterEvents		= gameObject.OnTriggerEnter2DAsObservable();
	}
}

