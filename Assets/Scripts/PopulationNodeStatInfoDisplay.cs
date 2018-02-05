using UnityEngine;

public class PopulationNodeStatInfoDisplay : MonoBehaviour {

	private PopulationNodeStats _populationNodeStats;

	private TextMesh _populationNodeStatDisplay;

	// Use this for initialization
	void Start () {
		_populationNodeStats = GetComponent<PopulationNodeStats>();
		_populationNodeStatDisplay = GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_populationNodeStatDisplay.text = 
			_populationNodeStats.Stats.PopulationNodeName 
			+ "\nPopulation: " + _populationNodeStats.Stats.Population
			+ "\nControl: " + _populationNodeStats.Stats.Control
			+ "\nSupport: " + _populationNodeStats.Stats.FactionsSupport[0] + " " + _populationNodeStats.Stats.FactionsSupport[1];
	}
}
