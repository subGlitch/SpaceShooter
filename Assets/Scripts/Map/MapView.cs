using System;
using System.Collections.Generic;
using UniRx;


public class MapView : UiViewBase
{
	public List< MapLocationView >	Locations;


	public IObservable< int > LocationSelect;


	void Start()
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

			MapLocationState state		=
											i <= 1 ? MapLocationState.Completed :
											i == 2 ? MapLocationState.Available :
											MapLocationState.Locked
			;
			location.SetState( state );
		}
	}
}

