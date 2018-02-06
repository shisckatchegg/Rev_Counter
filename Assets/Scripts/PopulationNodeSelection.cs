using UnityEngine;

public class PopulationNodeSelection : MonoBehaviour
{
	private PopulationNodeStats _populationNodeStats;   //This needs to be assigned when cliking on the population node 
	private SelectionDisplay _selectionDisplay;			//This will edit the text in the scene

	// Use this for initialization
	void Start () {
		_selectionDisplay = new SelectionDisplay();

		_selectionDisplay.Initialization();
		_selectionDisplay.FirstUpdate();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_selectionDisplay.Update();
	}


	//OnMouseDown goes here. This method would be caled when clicking on the population node
	//This method will then edit the text display of the bar
}
