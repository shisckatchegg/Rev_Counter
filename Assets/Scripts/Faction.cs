using System.Collections.Generic;
using UnityEngine;
using Events;

public class Faction
{
	public Globals.FactionNames FactionId;

	public List<GameObject> ControlledPopulationNodes;

	public List<SpyUnit> ControlledSpies;

	public List<SoldierUnit> ControlledMilitary;

	public GameObject ControlledLeader;

	public int[] FactionRelationShips;

	private FactionDisplay _factionDataDisplay;

    public PopulationNodeSelection Selection;
    
    public Faction(Globals.FactionNames factionId)
	{
		FactionId = factionId;
	}

	public void Initialize ()
	{
		ControlledPopulationNodes = new List<GameObject>();
		ControlledMilitary = new List<SoldierUnit>();
		ControlledSpies = new List<SpyUnit>();
		FactionRelationShips = new int[Globals.NumberOfFactions];
		_factionDataDisplay = new FactionDisplay();
		
		OnInitializationCollectControlledPopulationNodes();
		OnInitializationCollectControlledUnits();

		if (Globals.PlayerFaction == FactionId)
		{
			_factionDataDisplay.InitializeTextDisplay();
			_factionDataDisplay.FirstUpdate(FactionId, ControlledPopulationNodes.Count, ControlledSpies.Count, ControlledMilitary.Count);
		}

		EventManager.StartListening<UnitRecruitmentData>(EventNames.SpyRecruited, OnRecruitSpy);
	}
	
	public void ExecuteUnitMovementOrders()
	{
		for (int spyUnitIndex = 0; spyUnitIndex < ControlledSpies.Count; spyUnitIndex++)
		{
			if (ControlledSpies[spyUnitIndex].OrderedToMove)
			{
				ControlledSpies[spyUnitIndex].ExecuteMovement();
			}
		}

		for (int soldierUnitIndex = 0; soldierUnitIndex < ControlledMilitary.Count; soldierUnitIndex++)
		{
			ControlledMilitary[soldierUnitIndex].ExecuteMovement();
		}
	}

	public void ExecuteUnitAssassinationOrders()
	{
		for (int spyUnitIndex = 0; spyUnitIndex < ControlledSpies.Count; spyUnitIndex++)
		{
			if (ControlledSpies[spyUnitIndex].OrderedToAssassinate)
			{
				ControlledSpies[spyUnitIndex].ExecuteAssassination();
			}
		}
	}

	public void ExecutePropagandaOrders()
	{
		for (int spyUnitIndex = 0; spyUnitIndex < ControlledSpies.Count; spyUnitIndex++)
		{
			if (ControlledSpies[spyUnitIndex].OrderedToSpreadPropaganda)
			{
				ControlledSpies[spyUnitIndex].ExecutePropagandaCampaign();
			}
		}
	}

	public void Update ()
	{
		//Unit orders will go here
	}

	private void OnInitializationCollectControlledPopulationNodes()
	{
		GameObject[] populationNodes = GameObject.FindGameObjectsWithTag("PopulationNode");

		for (int populationNodeIndex = 0; populationNodeIndex < populationNodes.Length; populationNodeIndex++)
		{
			PopulationNode populationNodeStats = populationNodes[populationNodeIndex].GetComponent<PopulationNode>();
			if (populationNodeStats.Stats.Control == (int)FactionId)
			{
				ControlledPopulationNodes.Add(populationNodes[populationNodeIndex]);
			}
		}
	}

	private void OnInitializationCollectControlledUnits()
	{
		OnInitializeCollectControlledSpies();

		OnInitializeCollectControlledMilitary();

		OnInitializeCollectControlledLeader();
	}
    
	private void OnInitializeCollectControlledSpies()
	{
		GameObject[] populationNodeGameobjects = GameObject.FindGameObjectsWithTag("PopulationNode");

		if(populationNodeGameobjects == null)
		{
			Debug.Log("OnInitializeCollectControlledSpies - PopulationNode GameObjects not found when looking for controlled spies!");
			return;
		}

		for (int populationNodeIndex = 0; populationNodeIndex < populationNodeGameobjects.Length; populationNodeIndex++)
		{
			for (int spyIndex = 0; spyIndex < populationNodeGameobjects[populationNodeIndex].GetComponent<PopulationNode>().PresentSpies.Count; spyIndex++)
			{
				SpyUnit spyUnit = populationNodeGameobjects[populationNodeIndex].GetComponent<PopulationNode>().PresentSpies[spyIndex];
				if (spyUnit.Faction == FactionId)
				{
					ControlledSpies.Add(populationNodeGameobjects[populationNodeIndex].GetComponent<PopulationNode>().PresentSpies[spyIndex]);
				}
			}
		}
	}

	private void OnRecruitSpy(UnitRecruitmentData newUnitData)
	{
		if (newUnitData.NewlyRecruitedUnit is SpyUnit)
		{
			if (newUnitData.NewlyRecruitedUnit.Faction == FactionId)
			{
				ControlledSpies.Add((SpyUnit)newUnitData.NewlyRecruitedUnit);
			}

			if (Globals.PlayerFaction == FactionId)
			{
				_factionDataDisplay.Update(FactionId, ControlledPopulationNodes.Count, ControlledSpies.Count, ControlledMilitary.Count);
			}
		}
		else
		{
			Debug.Log("OnRecruitSpy - Unit data is not a spy!");
		}
	}

	private void OnInitializeCollectControlledMilitary()
	{
		GameObject[] populationNodeGameobjects = GameObject.FindGameObjectsWithTag("PopulationNode");

		if (populationNodeGameobjects == null)
		{
			Debug.Log("OnInitializeCollectControlledMilitary - PopulationNode GameObjects not found when looking for controlled military!");
			return;
		}

		for (int populationNodeIndex = 0; populationNodeIndex < populationNodeGameobjects.Length; populationNodeIndex++)
		{
			for (int soldierIndex = 0; soldierIndex < populationNodeGameobjects[populationNodeIndex].GetComponent<PopulationNode>().PresentSoldiers.Count; soldierIndex++)
			{
				SoldierUnit soldierUnit = populationNodeGameobjects[populationNodeIndex].GetComponent<PopulationNode>().PresentSoldiers[soldierIndex];
				if (soldierUnit.Faction == FactionId)
				{
					ControlledMilitary.Add(populationNodeGameobjects[populationNodeIndex].GetComponent<PopulationNode>().PresentSoldiers[soldierIndex]);
				}
			}
		}
	}

	private void OnInitializeCollectControlledLeader()
	{

	}


}
