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

	public enum PopulationSizes
	{
		Small = 0,
		Medium,
		Large,
	}
	
	public static Vector2 SmallMapSize = new Vector2(50, 50);
	public static Vector2 MediumMapSize = new Vector2(75, 75);
	public static Vector2 LargeMapSize = new Vector2(90, 90);

	public static Vector2 CurrentMapSize = SmallMapSize;
	public static MapSizes CurrentMapSizeType = MapSizes.Small;

	public static PopulationSizes CurrentPopulationSize = PopulationSizes.Small;


	public static FactionNames PlayerFaction = FactionNames.Soviets;

}
