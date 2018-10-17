using UnityEngine;

public class PopulationNodeStatInfoDisplay : MonoBehaviour {

	private PopulationNode _populationNode;

	private TextMesh _populationNodeStatDisplay;

	// Use this for initialization
	void Start () {
		_populationNode = GetComponent<PopulationNode>();
		_populationNodeStatDisplay = GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_populationNodeStatDisplay.text =
			_populationNode.Stats.PopulationNodeName
			+ "\nPopulation: " + _populationNode.Stats.Population
			//+ "\nControl: " + _populationNode.Stats.Control 
			+ "\nSupport: ";
		for (int factionSupportIndex = 0; factionSupportIndex < Globals.NumberOfFactions; factionSupportIndex++)
		{
			if(factionSupportIndex != 0)
			{
				_populationNodeStatDisplay.text += " - ";
			}
			_populationNodeStatDisplay.text += _populationNode.Stats.FactionsSupport[factionSupportIndex].ToString("0.00");
		}
	}
}
