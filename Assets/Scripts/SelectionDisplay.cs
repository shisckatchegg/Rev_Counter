using UnityEngine;
using UnityEngine.UI;

public class SelectionDisplay
{
	private Text _selectionText;
    
    public void Initialization()
	{
		_selectionText = GameObject.Find("SelectedPopulationNode").GetComponent<Text>();
        _selectionText.text = "";
    }

    public void FirstUpdate(PopulationNodeStats selection)
	{
        _selectionText.text =
            selection.PopulationNodeName
            + "\nPopulation: " + selection.Population
            + "\nControl: " + selection.Control
            + "\nSupport: " + selection.FactionsSupport[0];// + " " + selection.Stats.FactionsSupport[1];

        GameObject.Find("Recruitment").transform.localScale = new Vector3(1, 1, 1);
    }



    public void Update()
	{

	}
}
