using UnityEngine;
using System.Collections.Generic;

public class CityStatProcesser
{
	public GameObject [] PopulationNodes;
	
	public void Initialize()
	{
		PopulationNodes = GameObject.FindGameObjectsWithTag("PopulationNode");
	}

	public void Update()
	{
		foreach (var populatioNode in PopulationNodes)
		{
			populatioNode.GetComponent<PopulationNodeStats>().ProcessStats();
		}
	}
}
