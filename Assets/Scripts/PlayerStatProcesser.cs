public class PlayerStatProcesser
{
	public Faction[] GameFactions;

	public void PreInitialize()
	{
		GameFactions = new Faction[Globals.NumberOfFactions];

		for (int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
		{
			GameFactions[factionIndex] = new Faction((Globals.FactionNames)factionIndex);
		}
	}

	public void Initialize()
	{
		for (int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
		{
			GameFactions[factionIndex].Initialize();
		}
	}

	public void OnProcessTurn()
	{
		for (int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
		{
			GameFactions[factionIndex].ExecuteUnitAssassinationOrders();
			GameFactions[factionIndex].ExecutePropagandaOrders();
			GameFactions[factionIndex].ExecuteUnitMovementOrders();
            GameFactions[factionIndex].Update();
            GameFactions[factionIndex].Earnings();

        }
    }
}
