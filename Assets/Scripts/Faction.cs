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


	// Use this for initialization
	public void Initialize ()
	{
		ControlledPopulationNodes = new List<GameObject>();
		ControlledMilitary = new List<GameObject>();
		ControlledSpies = new List<GameObject>();
		FactionRelationShips = new int[(int) Globals.FactionNames.NumberOfFactions];
		
		CollectControlledPopulationNodes();
		CollectControlledUnits();
	}
	
	// Update is called once per frame
	public void Update ()
	{
		
	}

	private void CollectControlledPopulationNodes()
	{
		GameObject[] populationNodes = GameObject.FindGameObjectsWithTag("PopulationNode");
		
		for (int populationNodeIndex = 0; populationNodeIndex < populationNodes.Length; populationNodeIndex++)
		{
			PopulationNodeStats populationNodeStats = populationNodes[populationNodeIndex].GetComponent<PopulationNodeStats>();
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
}
