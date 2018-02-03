﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {

	public Globals.FactionNames FactionId;

	public List<GameObject> ControlledPopulationNodes;

	public List<GameObject> ControlledSpies;

	public List<GameObject> ControlledMilitary;

	public GameObject ControlledLeader;

	public int[] FactionRelationShips;


	// Use this for initialization
	void Start ()
	{
		ControlledPopulationNodes = new List<GameObject>();

		CollectControlledPopulationNodes();
	}
	
	// Update is called once per frame
	void Update () {
		
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

	}
}
