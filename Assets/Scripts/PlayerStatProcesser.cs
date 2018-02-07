public class PlayerStatProcesser
{
	private Faction[] _gameFactions;

	public void Initialize()
	{
		_gameFactions = new Faction[Globals.NumberOfFactions];

		for(int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
		{
			_gameFactions[factionIndex] = new Faction((Globals.FactionNames) factionIndex);
			_gameFactions[factionIndex].Initialize();
		}
	}

	public void Update()
	{
		for (int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
		{
			_gameFactions[factionIndex].Update();
		}
	}
}
