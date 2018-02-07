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
		//_selectionDisplay.FirstUpdate();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_selectionDisplay.Update();
	}

    void OnMouseDown()
    {
        _populationNodeStats = GetComponent<PopulationNodeStats>();
        _selectionDisplay.FirstUpdate(_populationNodeStats);
        //Debug.Log(_populationNodeStats.Stats.FactionsSupport[0]);
    }

    //OnMouseDown goes here. This method would be called when clicking on the population node
    //This method will then edit the text display of the bar
}
