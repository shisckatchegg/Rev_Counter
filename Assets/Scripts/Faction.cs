using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

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
    
    /*
    private UnityAction someListener;
    
    void Awake()
    {
        someListener = new UnityAction(RecruitSpy);
    }

    void SubscribeRecruitmentEvent()
    {
        EventManager.StartListening("test", someListener);
    }

    void UnsubscribeRecruitmentEvent()
    {
        EventManager.StopListening("test", someListener);
    }
    /*
    public void RecruitSpy()
    {
        Debug.Log("spy recruitment");

        //Selection.SelectedPopulationNode.PresentSpies.Add(new SpyUnit(Selection.SelectedPopulationNode, FactionId));
    }*/



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
		
		CollectControlledPopulationNodes();
		CollectControlledUnits();

		if (Globals.PlayerFaction == FactionId)
		{
			_factionDataDisplay.InitializeTextDisplay();
			_factionDataDisplay.FirstUpdate(FactionId, ControlledPopulationNodes.Count, ControlledSpies.Count, ControlledMilitary.Count);
		}
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
		if (Globals.PlayerFaction == FactionId)
		{
			_factionDataDisplay.Update();
		}

		CollectControlledUnits();
	}

	private void CollectControlledPopulationNodes()
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

	private void CollectControlledUnits()
	{
		CollectControlledSpies();

		CollectControlledMilitary();

		CollectControlledLeader();
	}
    
	private void CollectControlledSpies()
	{
		GameObject[] populationNodeGameobjects = GameObject.FindGameObjectsWithTag("PopulationNode");

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

	private void CollectControlledMilitary()
	{
		GameObject[] populationNodeGameobjects = GameObject.FindGameObjectsWithTag("PopulationNode");

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

	private void CollectControlledLeader()
	{

	}


}
