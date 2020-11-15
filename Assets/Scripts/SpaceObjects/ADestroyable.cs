

public abstract class ADestroyable
{
	public delegate void DestroyableEvent( ADestroyable destroyable );
	public event DestroyableEvent	OnDestroy;


	protected void Destroy()
	{
		DestroySilently();
		OnDestroy?.Invoke( this );
	}


	public abstract void DestroySilently();
}

