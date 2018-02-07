using UnityEngine;
using UnityEngine.UI;

public class SelectionDisplay
{
	private Text _selectionText;


	public void Initialization()
	{
		_selectionText = GameObject.Find("SelectedPopulationNode").GetComponent<Text>();
	}
	
	public void FirstUpdate(PopulationNodeStats selection)
	{
        _selectionText.text =
            selection.Stats.PopulationNodeName
            + "\nPopulation: " + selection.Stats.Population
            + "\nControl: " + selection.Stats.Control
            + "\nSupport: " + selection.Stats.FactionsSupport[0];// + " " + selection.Stats.FactionsSupport[1];
    }

    public void Update()
	{

	}
}
