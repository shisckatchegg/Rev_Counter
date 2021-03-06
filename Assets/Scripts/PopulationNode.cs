﻿using UnityEngine;
using System.Collections.Generic;
using Events;

public class PopulationNode : MonoBehaviour
{
	public PopulationNodeStats Stats;

	public List<SoldierUnit> PresentSoldiers;
    public List<SpyUnit> PresentSpies;

    public PopulationNodeSelection PopulationNodeSelection;

	public const float MINIMUM_SPY_RECRUITMENT_SUPPORT = 0.2f;

	private SpriteRenderer _flagRenderer;
	private GameObject _campSpriteGameObject;

	private void Awake()
	{
		PopulationNodeSelection = GameObject.Find("SelectedPopulationNodeText").GetComponent<PopulationNodeSelection>();

		_flagRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
		_campSpriteGameObject = transform.GetChild(3).gameObject;
	}

	// Use this for initialization
	void Start()
	{
        PresentSoldiers = new List<SoldierUnit>();
        PresentSpies = new List<SpyUnit>();
		UpdateFlagColor();
		DisplayCampIfMilitaryPresent();
	}


	public void PopulationNodeGraphicDisplayUpdate()
	{
		UpdateFlagColor();
		DisplayCampIfMilitaryPresent();
	}

    public void PopulationNodeUpdate()
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
		EventManager.TriggerEvent(EventNames.PopulationNodeSelected, new PopulationNodeSelectedEventData() { Control = Stats.Control });
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
		}
		else
		{
			Debug.Log("The faction: " + factionId.ToString() + " doesn´t have enough support to recruit a spy unit in: " + Stats.PopulationNodeName);
		}
	}

	public void RecruitSoldier(Globals.FactionNames factionId)
	{
		if (Stats.Control == (int) factionId)
		{
			SoldierUnit newSoldier = new SoldierUnit(this, factionId);
			PresentSoldiers.Add(newSoldier);
			Events.EventManager.TriggerEvent(EventNames.MilitaryRecruited, new UnitRecruitmentData() { NewlyRecruitedUnit = newSoldier });
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


	private void UpdateFlagColor()
	{
		switch((Globals.FactionNames) Stats.Control)
		{
			case Globals.FactionNames.Soviets:
				ApplyNewFlagColor(Color.red);
				break;
			case Globals.FactionNames.Tsarists:
				ApplyNewFlagColor(Color.blue);
				break;
			case Globals.FactionNames.SocialistRev:
				ApplyNewFlagColor(Color.grey);
				break;
			
		}
	}

	private void ApplyNewFlagColor(Color newColor)
	{
		_flagRenderer.color = newColor;
	}

	private void ToggleCampDisplay(bool shouldDisplayCamp)
	{
		_campSpriteGameObject.SetActive(shouldDisplayCamp);
	}

	private void DisplayCampIfMilitaryPresent()
	{
		if(PresentSoldiers != null)
		{
			ToggleCampDisplay(PresentSoldiers.Count > 0);
		}
		else
		{
			ToggleCampDisplay(false);
		}
	}
}
