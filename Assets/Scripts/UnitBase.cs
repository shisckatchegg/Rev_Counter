using UnityEngine;

public abstract class UnitBase
{
    public bool CreatedThisTurn;

	public Globals.FactionNames Faction;

    public PopulationNode CurrentLocation;

	public PopulationNode Destination;

    public UnitBase(PopulationNode location, Globals.FactionNames factionNames)
	{
        CurrentLocation = location;
		Faction = factionNames;
        CreatedThisTurn = true;
    }

	public abstract void InitiateMovement(PopulationNode destination);

	public abstract void ExecuteMovement();

}
