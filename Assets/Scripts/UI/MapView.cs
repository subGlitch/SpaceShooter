using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


public class MapView : UiViewBase
{
#pragma warning disable 0649

	[SerializeField] List< MapLocationView >	_locations;

#pragma warning restore 0649


	public IObservable< int > LocationSelect;


	void Start()
	{
		LocationSelect			= Observable.Empty< int >();

		for (int i = 0; i < _locations.Count; i ++)
		{
			var location		= _locations[ i ];

			location.SetLevelNum( i + 1 );

			int copy			= i;						// https://www.jetbrains.com/help/resharper/AccessToModifiedClosure.html
			LocationSelect		= Observable.Merge(
													LocationSelect,
													location.PressEvents.Select( _ => copy )
			);
		}
	}
}

