
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

		NumberOfFactions
	}

	public static int NumberOfFactions = 3;

	public static FactionNames PlayerFaction = FactionNames.Soviets;

}
