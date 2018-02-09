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
			+ "\nControl: " + _populationNode.Stats.Control
			+ "\nSupport: " + _populationNode.Stats.FactionsSupport[0] + " " + _populationNode.Stats.FactionsSupport[1];
	}
}
