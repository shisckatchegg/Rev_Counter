using UnityEngine;

public class CityStatProcesser
{
	public GameObject [] PopulationNodes;
	
	public void Initialize() 
	{
		PopulationNodes = GameObject.FindGameObjectsWithTag("PopulationNode");
	}

	public void OnProcessTurn()
	{
		foreach (var populatioNode in PopulationNodes)
		{
			populatioNode.GetComponent<PopulationNode>().PopulationNodeUpdate();
		}
	}

	public void OnProcessTurnGraphics()
	{
		foreach (var populatioNode in PopulationNodes)
		{
			populatioNode.GetComponent<PopulationNode>().PopulationNodeGraphicDisplayUpdate();
		}
	}
}
