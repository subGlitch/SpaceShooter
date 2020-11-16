using System;
using System.Collections.Generic;
using UniRx;


public class MapView : UiViewBase
{
	public List< MapLocationView >	Locations;


	public IObservable< int > LocationSelect;


	void Awake()
	{
		LocationSelect			= Observable.Empty< int >();

		for (int i = 0; i < Locations.Count; i ++)
		{
			var location		= Locations[ i ];

			location.SetLevelNum( i + 1 );

			int copy			= i;						// https://www.jetbrains.com/help/resharper/AccessToModifiedClosure.html
			LocationSelect		= Observable.Merge(
													LocationSelect,
													location.PressEvents.Select( _ => copy )
			);
		}
	}
}

