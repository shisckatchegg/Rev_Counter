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

	public static Vector2 CurrentMapSize = new Vector2(2048, 2048);
	public static MapSizes CurrentMapSizeType = MapSizes.Small;

	public static FactionNames PlayerFaction = FactionNames.Soviets;

}
