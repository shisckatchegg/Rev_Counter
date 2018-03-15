﻿using UnityEngine;
using System.Collections.Generic;

public class SpyUnit : UnitBase
{
	[HideInInspector]
	public int AssasinationBaseChance = 60;     //Chance of performing successful assasination
	[HideInInspector]
	public float PropagandaBaseEffectiveness = 0.03f;

	public bool OrderedToAssassinate;
	public bool OrderedToMove;

	public SpyUnit(PopulationNode location, Globals.FactionNames factionId)
		: base(location, factionId)
	{

	}

	// Use this for initialization
	protected override void Start ()
	{
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update();
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
		
		int randomAssassinationTarget = Random.Range(0, opposingSpies.Count - 1);

		if (Random.Range(0, 100) < AssasinationBaseChance)
		{
			CurrentLocation.PresentSpies.Remove(opposingSpies[randomAssassinationTarget]);
			Destroy(opposingSpies[randomAssassinationTarget]);
		}

		OrderedToAssassinate = false;
	}

	public void CounterEspionage()
	{

	}

	public void Infiltrate()
	{

	}

	public void PropagandaCampaign()
	{
		float currentSupport = CurrentLocation.Stats.GetFactionSupport(Faction);
		CurrentLocation.Stats.SetFactionSupport(Faction, currentSupport + PropagandaBaseEffectiveness);
	}

	public void BuildSpyNetwork()
	{

	}

	public bool IsSpyBusy()
	{
		return OrderedToAssassinate || OrderedToAssassinate;
	}
}
