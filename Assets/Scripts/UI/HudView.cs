using UnityEngine;
using UnityEngine.UI;


public class HudView : UiViewBase
{
#pragma warning disable 0649

	[SerializeField] Text		_hull;
	[SerializeField] Text		_timer;

#pragma warning restore 0649


	float	_timerEndTime;


	void Update()
	{
		UpdateTimerText();
	}


	public void SetHull( int hull )
	{
		_hull.text		= $"Hull: { hull }";
	}


	public void SetTimerEndTime( float timerEndTime )
	{
		_timerEndTime	= timerEndTime;
	}


	void UpdateTimerText()
	{
		int time		= Mathf.CeilToInt( Mathf.Max( _timerEndTime - Time.time, 0 ) );

		_timer.text		= $"Time: { time }";
	}
}

