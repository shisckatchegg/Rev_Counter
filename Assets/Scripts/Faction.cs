using System.Collections.Generic;
using UnityEngine;

public class Faction
{
	public Globals.FactionNames FactionId;

	public List<GameObject> ControlledPopulationNodes;

	public List<GameObject> ControlledSpies;

	public List<GameObject> ControlledMilitary;

	public GameObject ControlledLeader;

	public int[] FactionRelationShips;

	private FactionDisplay _factionDataDisplay;

    public PopulationNodeSelection selection;
    
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


		_factionDataDisplay.InitializeTextDisplay();
		_factionDataDisplay.FirstUpdate(FactionId, ControlledPopulationNodes.Count, ControlledSpies.Count, ControlledMilitary.Count);
	}
	
	public void Update ()
	{
		//Unit orders will go here

		_factionDataDisplay.Update();
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

	}

	private void CollectControlledMilitary()
	{

	}

	private void CollectControlledLeader()
	{

	}

    public void RecruitSpy()
    {
        Debug.Log("spy recruitment");
        
        selection._populationNode.PresentUnits.Add(new SpyUnit(selection._populationNode, FactionId));
    }
}
