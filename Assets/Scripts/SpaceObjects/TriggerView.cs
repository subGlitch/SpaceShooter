using UniRx.Triggers;
using UnityEngine;


public class TriggerView : MonoBehaviour
{
	public System.IObservable< Collider2D >		TriggerEnterEvents;
	public System.IObservable< Collider2D >		TriggerExitEvents;


	void Awake()
	{
		TriggerEnterEvents		= gameObject.OnTriggerEnter2DAsObservable();
		TriggerExitEvents		= gameObject.OnTriggerExit2DAsObservable();
	}
}

