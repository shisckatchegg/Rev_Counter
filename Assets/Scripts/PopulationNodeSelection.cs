using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Events;

public struct UnitRecruitmentData
{
	public UnitBase NewlyRecruitedUnit;
}

public class PopulationNodeSelection : MonoBehaviour
{
	public PopulationNode SelectedPopulationNode;       //This needs to be assigned when cliking on the population node 

	private SelectionDisplay _selectionDisplay;         //This will edit the text in the scene

	private List<SpyUnit> _ownFactionPresentSpies;
	private List<SoldierUnit> _ownFactionPresentSoldiers;

	private GameObject _leftPanel;
	private GameObject _factionRelations;

	private GameObject _declareWarButton;
	private GameObject _offerAllianceButton;
	private GameObject _askToJoinFederationButton;

	private DiplomaticActions _diplomaticActions;

	private Text _factionRelationsText;

	private void Awake()
	{
		_selectionDisplay = new SelectionDisplay();
		_diplomaticActions = new DiplomaticActions();

		_selectionDisplay.PreInitialization();

		_leftPanel = GameObject.Find("LeftPanel");
		_factionRelations = GameObject.Find("FactionRelations");

		_declareWarButton = GameObject.Find("DeclareWarButton");
		_offerAllianceButton = GameObject.Find("OfferAllianceButton");
		_askToJoinFederationButton = GameObject.Find("AskToJoinFederation");

		_factionRelationsText = GameObject.Find("FactionRelations").GetComponent<Text>();
	}

	// Use this for initialization
	void Start ()
	{
		_selectionDisplay.Initialization();

		_leftPanel.SetActive(false);
		_factionRelations.SetActive(false);

		_declareWarButton.SetActive(false);
		_offerAllianceButton.SetActive(false);
		_askToJoinFederationButton.SetActive(false);
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
			DisplayFactionRelationsWithPlayer((Globals.FactionNames)SelectedPopulationNode.Stats.Control);
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

		if (SelectedPopulationNode.Stats.Control != (int)Globals.PlayerFaction)
		{
			_leftPanel.SetActive(true);
			_factionRelations.SetActive(true);

			_declareWarButton.SetActive(true);
			_offerAllianceButton.SetActive(true);
			_askToJoinFederationButton.SetActive(true);
		}
		else
		{
			_leftPanel.SetActive(false);
			_factionRelations.SetActive(false);

			_declareWarButton.SetActive(false);
			_offerAllianceButton.SetActive(false);
			_askToJoinFederationButton.SetActive(false);
		}
	}

	private List<UnitBase> FilterFactionUnits(List<UnitBase> presentUnits)
	{
		List<UnitBase> ownFactionPresentUnits = new List<UnitBase>();

		for(int unitIndex = 0; unitIndex < presentUnits.Count; unitIndex++)
		{
			if(presentUnits[unitIndex].Faction == Globals.PlayerFaction)
			{
				ownFactionPresentUnits.Add(presentUnits[unitIndex]);
			}
		}

		return ownFactionPresentUnits;
	}

	public void InitiateMovement(PopulationNode destination)
	{
		_selectionDisplay.SubmitSelection();
		int spyIndex = 0;
		int soldierIndex = 0;
		while (spyIndex < _selectionDisplay.SelectedSpies)
		{
			if (!SelectedPopulationNode.PresentSpies[spyIndex].IsSpyBusy())
			{
				SelectedPopulationNode.PresentSpies[spyIndex].InitiateMovement(destination);
			}
			spyIndex++;
		}

		
		while (soldierIndex < _selectionDisplay.SelectedSoldiers)
		{
			//if (!SelectedPopulationNode.PresentSoldiers[soldierIndex].IsSpyBusy())
			//{
				SelectedPopulationNode.PresentSoldiers[soldierIndex].InitiateMovement(destination);
			//}
			soldierIndex++;
		}

		_selectionDisplay.SelectedSoldiers = 0;

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
				SelectedPopulationNode.PresentSpies[spyIndex].OrderedToAssassinate = true;
			}
			spyIndex++;
		}

		_selectionDisplay.SelectedSpies = 0;
	}

	public void InitiatePropagandaCampaign()
	{
		_selectionDisplay.SubmitSelection();
        int spyIndex = 0;
        int readySpyIndex = 0;

        while (spyIndex < SelectedPopulationNode.PresentSpies.Count)
		{
			if (!SelectedPopulationNode.PresentSpies[spyIndex].IsSpyBusy())
			{
                while (readySpyIndex < _selectionDisplay.SelectedSpies)
                {
                    SelectedPopulationNode.PresentSpies[spyIndex].OrderToSpreadPropaganda();
                    readySpyIndex++;
                }
			}
			spyIndex++;
		}

		_selectionDisplay.SelectedSpies = 0;
	}

    public void PlayerSpyRecruitment()
    {
        SelectedPopulationNode.RecruitSpy(Globals.PlayerFaction);
    }

	public void PlayerSoldierRecruitment()
	{
		SelectedPopulationNode.RecruitSoldier(Globals.PlayerFaction);
	}

	public void DisplayFactionRelationsWithPlayer(Globals.FactionNames factionId)
	{
		int playerOnFaction = -1;
		int factionOnPlayer = -1;
		FactionRelations.GetRelationsWithPlayer(factionId, out playerOnFaction, out factionOnPlayer);

		_factionRelationsText.text = "\tFaction Relations: \n" + "Our views on them: " + playerOnFaction + "\nTheir views on us: " + factionOnPlayer
			+ "\n Current status: " + FactionRelations.GetRelationStatusWithPlayer(factionId);
	}


	//public void DisplayAllFactionRelations()
	//{
	//	_factionRelationsText.text = "\tFaction Relations: \n";
	//	for (int foreignFactionIndex = 0; foreignFactionIndex < _factionRelations[(int)Globals.PlayerFaction].Length; foreignFactionIndex++)
	//	{
	//		if (_factionRelations[(int)Globals.PlayerFaction][foreignFactionIndex] != -1)
	//		{
	//			_factionRelationsText.text += ((Globals.FactionNames)foreignFactionIndex).ToString() + ": " + _factionRelations[(int)Globals.PlayerFaction][foreignFactionIndex] + "\n";
	//		}
	//	}
	//}

	public void PlayerDeclareWar()
	{
		_diplomaticActions.DeclareWarToFaction((Globals.FactionNames)SelectedPopulationNode.Stats.Control);
	}

	public void PlayerOfferPeace()
	{
		_diplomaticActions.OfferPeaceToFaction((Globals.FactionNames)SelectedPopulationNode.Stats.Control);
	}

	public void PlayerOfferAlliance()
	{
		_diplomaticActions.OfferAllianceToFaction((Globals.FactionNames)SelectedPopulationNode.Stats.Control);
	}

	public void PlayerAskToJoinFederation()
	{
		_diplomaticActions.AskToJoinFederation((Globals.FactionNames)SelectedPopulationNode.Stats.Control);
	}
}
