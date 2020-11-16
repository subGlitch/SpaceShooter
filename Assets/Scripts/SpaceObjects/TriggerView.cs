using UniRx.Triggers;
using UnityEngine;


public class TriggerView : MonoBehaviour
{
	public System.IObservable< Collider2D >		TriggerEvents;


	void Awake()
	{
		TriggerEvents		= gameObject.OnTriggerEnter2DAsObservable();
	}
}

