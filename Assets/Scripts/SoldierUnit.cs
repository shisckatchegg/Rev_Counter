using UnityEngine;

public class SoldierUnit : UnitBase
{
	public SoldierUnit(PopulationNode location, Globals.FactionNames factionId)
		: base(location, factionId)
	{

	}

	public override void InitiateMovement(PopulationNode destination)
	{
		Destination = destination;
	}

	public override void ExecuteMovement()
	{
		if(Destination != null)
		{
			CurrentLocation = Destination;
			Destination = null;
		}
	}
}
