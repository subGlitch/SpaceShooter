

public abstract class ADestroyableController
{
	public delegate void DestroyableEvent( ADestroyableController destroyableController );
	public event DestroyableEvent	OnDestroy;


	protected void Destroy()
	{
		DestroySilently();
		OnDestroy?.Invoke( this );
	}


	public abstract void DestroySilently();
}

