using UnityEngine;


public class Refs : MB_Singleton< Refs >
{
	[Header( "Prefabs" )]
	public ShipView			ShipViewPrefab;
	public AsteroidView		AsteroidViewPrefab;
	public BulletView		BulletViewPrefab;

	[Header( "Refs" )]
	public Transform		ShipSpawnPos;
	public Transform		Gameplay;

	[Header( "UI Refs" )]
	public HudView			HudView;
	public PopupPanelView	PopupPanelView;
}

