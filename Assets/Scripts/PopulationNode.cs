﻿using UnityEngine;
using System.Collections.Generic;

public class PopulationNode : MonoBehaviour
{
	public PopulationNodeStats Stats;

	public List<SoldierUnit> PresentSoldiers;
	public List<SpyUnit> PresentSpies;

	public PopulationNodeSelection PopulationNodeSelection;

	public const float MINIMUM_SPY_RECRUITMENT_SUPPORT = 0.2f;

	private void Awake()
	{
		PopulationNodeSelection = GameObject.Find("SelectedPopulationNodeText").GetComponent<PopulationNodeSelection>();
	}

	// Use this for initialization
	void Start()
	{
        PresentSoldiers = new List<SoldierUnit>();
        PresentSpies = new List<SpyUnit>();

    }


    public void ProcessStats()
	{
		Stats.Update();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseDown()
	{
		PopulationNodeSelection.InitializeSelection(this);
	}

	private void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(1))
		{
			PopulationNodeSelection.InitiateMovement(this);
			Debug.Log("City destination selected to move unit: " + Stats.PopulationNodeName);
		}
	}

	public void RecruitSpy(Globals.FactionNames factionId)
	{
		if (Stats.GetFactionSupport(factionId) >= MINIMUM_SPY_RECRUITMENT_SUPPORT)
		{
			SpyUnit newSpy = new SpyUnit(this, factionId);
			PresentSpies.Add(newSpy);
			Events.EventManager.TriggerEvent(EventNames.SpyRecruited, new UnitRecruitmentData() { NewlyRecruitedUnit = newSpy });
			if (Globals.PlayerFaction == factionId)
			{
				Debug.Log("-500");
				//Faction. -= 500;
			}
		}
		else
		{
			Debug.Log("The faction: " + factionId.ToString() + " doesn´t have enough support to recruit a spy unit in: " + Stats.PopulationNodeName);
		}
	}

	public List<SpyUnit> GetFactionSpies(Globals.FactionNames factionId)
	{
		List<SpyUnit> controlledSpies = new List<SpyUnit>();

		for(int spyIndex = 0; spyIndex < PresentSpies.Count; spyIndex++)
		{
			if(PresentSpies[spyIndex].Faction == factionId)
			{
				controlledSpies.Add(PresentSpies[spyIndex]);
			}
		}

		return controlledSpies;
	}

	public List<SoldierUnit> GetFactionMilitary(Globals.FactionNames factionId)
	{
		List<SoldierUnit> controlledMilitary = new List<SoldierUnit>();

		for (int soldierUnitIndex = 0; soldierUnitIndex < PresentSoldiers.Count; soldierUnitIndex++)
		{
			if (PresentSoldiers[soldierUnitIndex].Faction == factionId)
			{
				controlledMilitary.Add(PresentSoldiers[soldierUnitIndex]);
			}
		}

		return controlledMilitary;
	}

}
