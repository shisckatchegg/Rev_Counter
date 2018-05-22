using UnityEngine;
using System.Collections.Generic;

public struct SpyOdersData
{
    public SpyUnit OrderedSpy;
}

public class SpyUnit : UnitBase
{
	[HideInInspector]
	public int AssasinationBaseChance = 60;     //Chance of performing successful assasination
	[HideInInspector]
	public float PropagandaBaseEffectiveness = 0.03f;

	public bool OrderedToAssassinate;
	public bool OrderedToMove;
	private bool _orderedToSpreadPropaganda;

    public bool HasPropagandaBeenOredered(){
        return _orderedToSpreadPropaganda;
    }

	public SpyUnit(PopulationNode location, Globals.FactionNames factionId)
		: base(location, factionId)
	{

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
			for (int spyIndex = 0; spyIndex < CurrentLocation.PresentSpies.Count; spyIndex++)
			{
				if(CurrentLocation.PresentSpies[spyIndex] == this)
				{
					CurrentLocation.PresentSpies.Remove(this);
				}
			}

			Destination.PresentSpies.Add(this);

			CurrentLocation = Destination;
			Destination = null;

			OrderedToMove = false;
		}
	}

	public void ExecuteAssassination()
	{
		List<SpyUnit> opposingSpies = new List<SpyUnit>();

		for(int spyIndex = 0; spyIndex < CurrentLocation.PresentSpies.Count; spyIndex++)
		{
			if (CurrentLocation.PresentSpies[spyIndex].Faction != Faction)
			{
				opposingSpies.Add(CurrentLocation.PresentSpies[spyIndex]);
			}	
		}
		
		if(opposingSpies.Count == 0)
		{
			Debug.Log("Tried to assassinate in town where there are no enemy spies: " + CurrentLocation.Stats.PopulationNodeName);
			return;
		}

		int randomAssassinationTarget = Random.Range(0, opposingSpies.Count - 1);

		if (Random.Range(0, 100) < AssasinationBaseChance)
		{
			CurrentLocation.PresentSpies.Remove(opposingSpies[randomAssassinationTarget]);
			opposingSpies[randomAssassinationTarget] = null;
		}

		OrderedToAssassinate = false;
	}

	public void CounterEspionage()
	{

	}

	public void Infiltrate()
	{

	}

	public void ExecutePropagandaCampaign()
	{
		float currentSupport = CurrentLocation.Stats.GetFactionSupport(Faction);
		CurrentLocation.Stats.SetFactionSupport(Faction, currentSupport + PropagandaBaseEffectiveness);
		_orderedToSpreadPropaganda = false;

        //Funds -= 500;
        //Debug.Log("Faction: " + FactionId + " spreaded propaganda: -500");
    }

    public void BuildSpyNetwork()
	{

	}

	public bool IsSpyBusy()
	{
		return OrderedToAssassinate || OrderedToMove || _orderedToSpreadPropaganda || CreatedThisTurn;
	}

    public void OrderToSpreadPropaganda()
    {
        _orderedToSpreadPropaganda = true;
        Events.EventManager.TriggerEvent(EventNames.OrderPropaganda, new SpyOdersData() { OrderedSpy = this });

    }

    public void CancelPropagandaOrder()
    {
        _orderedToSpreadPropaganda = false;
    }
}
