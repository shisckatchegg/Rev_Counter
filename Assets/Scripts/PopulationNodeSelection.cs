using UnityEngine;
using System.Collections;

public class PopulationNodeSelection : MonoBehaviour
{
	public PopulationNode SelectedPopulationNode;       //This needs to be assigned when cliking on the population node 

	private SelectionDisplay _selectionDisplay;         //This will edit the text in the scene

	private void Awake()
	{
		_selectionDisplay = new SelectionDisplay();
		_selectionDisplay.PreInitialization();
	}

	// Use this for initialization
	void Start ()
	{
		_selectionDisplay.Initialization();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void InitializeSelection(PopulationNode populationNode)
	{
		SelectedPopulationNode = populationNode;
		_selectionDisplay.FirstUpdate(SelectedPopulationNode.Stats.PopulationNodeName, SelectedPopulationNode.PresentSoldiers, SelectedPopulationNode.PresentSpies);
	}

	public void InitiateMovement(PopulationNode destination)
	{
		_selectionDisplay.SubmitSelection();
		int spyIndex = 0;
		while(spyIndex < _selectionDisplay.SelectedSpies)
		{
			if (!SelectedPopulationNode.PresentSpies[spyIndex].IsSpyBusy())
			{
				SelectedPopulationNode.PresentSpies[spyIndex].InitiateMovement(destination);
			}
			spyIndex++;
		}

		_selectionDisplay.SelectedSpies = 0;
	}

	public void InitiateAssassination()
	{
		_selectionDisplay.SubmitSelection();
		int spyIndex = 0;
		while (spyIndex < _selectionDisplay.SelectedSpies)
		{
			if (!SelectedPopulationNode.PresentSpies[spyIndex].IsSpyBusy())
			{
				SelectedPopulationNode.PresentSpies[spyIndex].OrderedToAssassinate = true; ;
			}
			spyIndex++;
		}

		_selectionDisplay.SelectedSpies = 0;
	}
}
