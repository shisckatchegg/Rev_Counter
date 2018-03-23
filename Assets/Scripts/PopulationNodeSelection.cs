using UnityEngine;
using System.Collections.Generic;

public class PopulationNodeSelection : MonoBehaviour
{
	public PopulationNode SelectedPopulationNode;       //This needs to be assigned when cliking on the population node 

	private SelectionDisplay _selectionDisplay;         //This will edit the text in the scene

	private List<SpyUnit> _ownFactionPresentSpies;
	private List<SoldierUnit> _ownFactionPresentSoldiers;

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

		//_ownFactionPresentSpies = FilterFactionUnits(SelectedPopulationNode.PresentSpies);
		//_ownFactionPresentSoldiers = FilterFactionUnits(SelectedPopulationNode.PresentSoldiers);

		_selectionDisplay.FirstUpdate(SelectedPopulationNode.Stats.PopulationNodeName, SelectedPopulationNode.PresentSoldiers, SelectedPopulationNode.PresentSpies);
	}

	private List<UnitBase> FilterFactionUnits(List<UnitBase> presentUnits)
	{
		List<UnitBase> _ownFactionPresentUnits = new List<UnitBase>();

		for(int unitIndex = 0; unitIndex < presentUnits.Count; unitIndex++)
		{
			if(presentUnits[unitIndex].Faction == Globals.PlayerFaction)
			{
				_ownFactionPresentUnits.Add(presentUnits[unitIndex]);
			}
		}

		return _ownFactionPresentUnits;
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
