using UnityEngine;
using System.Collections;

public class PopulationNodeSelection : MonoBehaviour
{
    public PopulationNode _populationNode;

    //public PopulationNodeStats _populationNodeStats;   //This needs to be assigned when cliking on the population node 

    private SelectionDisplay _selectionDisplay;         //This will edit the text in the scene
    
    // Use this for initialization
    void Start () {
		_selectionDisplay = new SelectionDisplay();
		_selectionDisplay.Initialization();
    }

    // Update is called once per frame
    void Update ()
	{

	}

    void OnMouseDown()
    {
        _populationNode = GetComponent<PopulationNode>();
        //_populationNodeStats = GetComponent<PopulationNode>().Stats;
        _selectionDisplay.FirstUpdate(_populationNode.Stats);
    }
}
