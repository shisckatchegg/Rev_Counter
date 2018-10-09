using UnityEngine;
using System.Collections.Generic;

public class SoldierUnit : UnitBase
{
    public bool OrderedToMove;

    public SoldierUnit(PopulationNode location, Globals.FactionNames factionId)
    : base(location, factionId)
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void InitiateMovement(PopulationNode destination)
    {
        OrderedToMove = true;
        Destination = destination;
    }

    public override void ExecuteMovement()
    {
        if (Destination != null)
        {
            for (int soldierIndex = 0; soldierIndex < CurrentLocation.PresentSoldiers.Count; soldierIndex++)
            {
                if (CurrentLocation.PresentSoldiers[soldierIndex] == this)
                {
                    CurrentLocation.PresentSoldiers.Remove(this);
                }
            }

            Destination.PresentSoldiers.Add(this);

            CurrentLocation = Destination;
            Destination = null;

            OrderedToMove = false;

			if(CurrentLocation.Stats.Control != (int) Faction)
			{
				ConquerPopulationNode(CurrentLocation);
			}
        }
    }

	private void ConquerPopulationNode(PopulationNode target)
	{
		target.Stats.Control = (int)Faction;
	}
}
