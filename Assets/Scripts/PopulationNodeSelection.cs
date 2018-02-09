using UnityEngine;
using System.Collections;

public class PopulationNodeSelection : MonoBehaviour
{
	private PopulationNodeStats _populationNodeStats;   //This needs to be assigned when cliking on the population node 
	private SelectionDisplay _selectionDisplay;			//This will edit the text in the scene

	// Use this for initialization
	void Start () {
		_selectionDisplay = new SelectionDisplay();

		_selectionDisplay.Initialization();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_selectionDisplay.Update();
	}

    void OnMouseDown()
    {
        _populationNodeStats = GetComponent<PopulationNode>().Stats;
        _selectionDisplay.FirstUpdate(_populationNodeStats);
    }
}
