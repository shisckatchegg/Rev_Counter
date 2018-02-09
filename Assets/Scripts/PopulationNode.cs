using UnityEngine;
using System.Collections.Generic;

public class PopulationNode : MonoBehaviour
{
	public PopulationNodeStats Stats;

	public List<UnitBase> PresentUnits; 

	// Use this for initialization
	void Start()
	{
	}


	public void ProcessStats()
	{
		Stats.Update();

		Debug.Log("City: " + Stats.PopulationNodeName + " current population: " + Stats.Population);
		Debug.Log("City: " + Stats.PopulationNodeName + " support for faction 1: " + Stats.FactionsSupport[0]);
		Debug.Log("City: " + Stats.PopulationNodeName + " support for faction 2: " + Stats.FactionsSupport[1]);

	}

	// Update is called once per frame
	void Update()
	{

	}
}
