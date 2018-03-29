using UnityEngine;

public class UnitStatProcesser
{
    private Faction[] _gameFactions;

    public void Initialize()
    {
        _gameFactions = new Faction[Globals.NumberOfFactions];

        for (int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
        {
            _gameFactions[factionIndex] = new Faction((Globals.FactionNames)factionIndex);
            _gameFactions[factionIndex].Initialize();
        }
    }

    public void Update()
	{
        // cambiar a false las unidades preparadas, de todas las facciones
        for (int factionIndex = 0; factionIndex < Globals.NumberOfFactions; factionIndex++)
        {
            for (int spyUnitIndex = 0; spyUnitIndex < _gameFactions[factionIndex].ControlledSpies.Count; spyUnitIndex++)
            {
                _gameFactions[factionIndex].ControlledSpies[spyUnitIndex].CreatedThisTurn = false;
            }
        }
    }
}
