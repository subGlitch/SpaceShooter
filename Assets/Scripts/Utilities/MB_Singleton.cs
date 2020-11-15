using UnityEngine;


public class MB_Singleton< T > : MonoBehaviour where T : MB_Singleton< T >
{
	public static T Instance { get; private set; }


	protected virtual void Awake()
	{
		Instance		= Utils.SingletonPattern( (T)this, Instance );
	}


	void OnDestroy()
	{
		Instance		= null;
	}
}

