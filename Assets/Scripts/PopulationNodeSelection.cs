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
		if (_selectionDisplay != null && SelectedPopulationNode != null)
		{
			//TODO: very very inefficient!
			List<UnitBase> copiedListOfSpies;
			List<UnitBase> copiedListOfSoldiers;
			copiedListOfSpies = FilterFactionUnits(SelectedPopulationNode.PresentSpies.ConvertAll(x => (UnitBase)x));
			copiedListOfSoldiers = FilterFactionUnits(SelectedPopulationNode.PresentSoldiers.ConvertAll(x => (UnitBase)x));
			_ownFactionPresentSpies = copiedListOfSpies.ConvertAll(x => (SpyUnit)x);
			_ownFactionPresentSoldiers = copiedListOfSoldiers.ConvertAll(x => (SoldierUnit)x);

			_selectionDisplay.Update(_ownFactionPresentSoldiers.Count, _ownFactionPresentSpies.Count);
		}
	}

	public void InitializeSelection(PopulationNode populationNode)
	{
		SelectedPopulationNode = populationNode;

		//TODO: very inefficient!
		List<UnitBase> copiedListOfSpies;
		List<UnitBase> copiedListOfSoldiers;
		copiedListOfSpies = FilterFactionUnits(SelectedPopulationNode.PresentSpies.ConvertAll(x => (UnitBase)x));
		copiedListOfSoldiers = FilterFactionUnits(SelectedPopulationNode.PresentSoldiers.ConvertAll(x =>(UnitBase)x));
		_ownFactionPresentSpies = copiedListOfSpies.ConvertAll(x => (SpyUnit)x);
		_ownFactionPresentSoldiers = copiedListOfSoldiers.ConvertAll(x => (SoldierUnit)x);


		_selectionDisplay.FirstUpdate(SelectedPopulationNode.Stats.PopulationNodeName, _ownFactionPresentSoldiers, _ownFactionPresentSpies);
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

    public void RecruitSpy(Globals.FactionNames factionId)
    {
        SelectedPopulationNode.PresentSpies.Add(new SpyUnit(SelectedPopulationNode, factionId));
    }

    public void PlayerSpyRecruitment()
    {
        RecruitSpy(Globals.PlayerFaction);
    }
}
