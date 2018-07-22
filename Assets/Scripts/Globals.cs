using UnityEngine;

public static class Globals
{
	public enum FactionNames
	{
		Invalid = -1,
		//National factions
		Soviets,
		Tsarists,
		SocialistRev,

		//Foreign factions
		Thaarax,
		Kyslev,
		Man,

		MaxNumberOfFactions
	}

	public static int NumberOfFactions = 3;

	public enum MapSizes
	{
		Small = 0,
		Medium,
		Large,
	}

	public static Vector2 SmallMapSize = new Vector2(200, 200);
	public static Vector2 MediumMapSize = new Vector2(300, 300);
	public static Vector2 LargeMapSize = new Vector2(500, 500);

	public static Vector2 CurrentMapSize = SmallMapSize;
	public static MapSizes CurrentMapSizeType = MapSizes.Small;

	public static FactionNames PlayerFaction = FactionNames.Soviets;

}
