
public class UnitStatProcesser
{
    private Faction[] _gameFactions;

    public void Initialize(Faction[] GameFactions)
    {
		_gameFactions = GameFactions;
	}

    public void OnProcessTurn()
	{
        for (int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
        {
            for (int spyUnitIndex = 0; spyUnitIndex < _gameFactions[factionIndex].ControlledSpies.Count; spyUnitIndex++)
            {
                _gameFactions[factionIndex].ControlledSpies[spyUnitIndex].CreatedThisTurn = false;
            }

			for (int soldierUnitIndex = 0; soldierUnitIndex < _gameFactions[factionIndex].ControlledMilitary.Count; soldierUnitIndex++)
			{
				_gameFactions[factionIndex].ControlledMilitary[soldierUnitIndex].CreatedThisTurn = false;
			}
		}
    }
}
