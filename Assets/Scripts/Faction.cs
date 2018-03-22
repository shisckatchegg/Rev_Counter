using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Faction
{
	public Globals.FactionNames FactionId;

	public List<GameObject> ControlledPopulationNodes;

	public List<GameObject> ControlledSpies;

	public List<GameObject> ControlledMilitary;

	public GameObject ControlledLeader;

	public int[] FactionRelationShips;

	private FactionDisplay _factionDataDisplay;

    public PopulationNodeSelection Selection;

    private UnityAction someListener;
    /*
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
		ControlledMilitary = new List<GameObject>();
		ControlledSpies = new List<GameObject>();
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
			SpyUnit spy = ControlledSpies[spyUnitIndex].GetComponent<SpyUnit>();
			if (spy.OrderedToMove)
			{
				spy.ExecuteMovement();
			}
		}

		for (int soldierUnitIndex = 0; soldierUnitIndex < ControlledMilitary.Count; soldierUnitIndex++)
		{
			SoldierUnit soldier = ControlledMilitary[soldierUnitIndex].GetComponent<SoldierUnit>();
			soldier.ExecuteMovement();
		}
	}

	public void ExecuteUnitAssassinationOrders()
	{
		for (int spyUnitIndex = 0; spyUnitIndex < ControlledSpies.Count; spyUnitIndex++)
		{
			SpyUnit spy = ControlledSpies[spyUnitIndex].GetComponent<SpyUnit>();
			if (spy.OrderedToAssassinate)
			{
				spy.ExecuteAssassination();
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
		//GameObject[] spyGameObjects = GameObject.FindGameObjectsWithTag("SpyUnit");

		//for (int spyGameObjectIndex = 0; spyGameObjectIndex < spyGameObjects.Length; spyGameObjectIndex++)
		//{
		//	SpyUnit spyUnit = spyGameObjects[spyGameObjectIndex].GetComponent<SpyUnit>();
		//	if (spyUnit.Faction == FactionId)
		//	{
		//		ControlledSpies.Add(spyGameObjects[spyGameObjectIndex]);
		//	}
		//}
	}

	private void CollectControlledMilitary()
	{
		GameObject[] soldierGameObjects = GameObject.FindGameObjectsWithTag("SoldierUnit");

		for (int soldierGameObjectIndex = 0; soldierGameObjectIndex < soldierGameObjects.Length; soldierGameObjectIndex++)
		{
			SoldierUnit soldierUnit = soldierGameObjects[soldierGameObjectIndex].GetComponent<SoldierUnit>();
			if (soldierUnit.Faction == FactionId)
			{
				ControlledMilitary.Add(soldierGameObjects[soldierGameObjectIndex]);
			}
		}
	}

	private void CollectControlledLeader()
	{

	}


}
