using System.Collections.Generic;


public static class Bookkeeper
{
	static HashSet< ADestroyableController >	_spaceObjects		= new HashSet< ADestroyableController >();
	

	public static void Clear()
	{
		foreach (ADestroyableController spaceObject in _spaceObjects)
			spaceObject.DestroySilently();

		_spaceObjects.Clear();
	}


	public static void Register( ADestroyableController controller )
	{
		_spaceObjects.Add( controller );

		controller.OnDestroy		+= x => _spaceObjects.Remove( x );
	}
}

