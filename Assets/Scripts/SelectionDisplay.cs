using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectionDisplay
{
	private Text _populationNodeSelectedText;

	private GameObject _spySelectionGameObject;
	private GameObject _assassinateButtonGameObject;
	private GameObject _propagandaButtonGameObject;
	private GameObject _soldierSelectionGameObject;
	private GameObject _repressButtonGameObject;

	private Text _maximumSpyText;
	private Text _maximumSoldierText;

	private InputField _spySelection;
	private InputField _soldierSelection;

    private GameObject _recruitSpyButton;
    private GameObject _recruitSoldierButton;

	private Button _assassinateButton;
	private Button _propagandaButton;
	private Button _repressButton;

	public int SelectedSpies;
	public int SelectedSoldiers;

	public void PreInitialization()
	{
		_assassinateButtonGameObject = GameObject.Find("AssassinateButton");
		_assassinateButton = _assassinateButtonGameObject.GetComponent<Button>();

		_propagandaButtonGameObject = GameObject.Find("PropagandaButton");
		_propagandaButton = _propagandaButtonGameObject.GetComponent<Button>();

		_populationNodeSelectedText = GameObject.Find("SelectedPopulationNodeText").GetComponent<Text>();

		_spySelectionGameObject = GameObject.Find("SpyCounter");
		_maximumSpyText = GameObject.Find("AmountOfSpiesPresent").GetComponent<Text>();
		_spySelection = _spySelectionGameObject.GetComponentInChildren<InputField>();
		_spySelection.onValidateInput = InputValidation;
		_spySelection.onEndEdit.AddListener(delegate { CheckSpyUnitSelectionIsValid(_spySelection); } );
        _recruitSpyButton = GameObject.Find("Recruitment");

		_soldierSelectionGameObject = GameObject.Find("SoldierCounter");
		_maximumSoldierText = GameObject.Find("AmountOfSoldiersPresent").GetComponent<Text>();
		_soldierSelection = _soldierSelectionGameObject.GetComponentInChildren<InputField>();
		_soldierSelection.onValidateInput = InputValidation;
		_soldierSelection.onEndEdit.AddListener(delegate { CheckSoldierUnitSelectionIsValid(_soldierSelection); });
		_recruitSoldierButton = GameObject.Find("SoldierRecruitment");

		_repressButtonGameObject = GameObject.Find("RepressButton");
		_repressButton = _repressButtonGameObject.GetComponent<Button>();
    }

	public void Initialization()
	{
		_spySelectionGameObject.SetActive(false);
		_assassinateButtonGameObject.SetActive(false);
		_propagandaButtonGameObject.SetActive(false);

		_populationNodeSelectedText.text = "";

        _recruitSpyButton.SetActive(false);
		_recruitSoldierButton.SetActive(false);

		_repressButtonGameObject.SetActive(false);
		_soldierSelectionGameObject.SetActive(false);

	}
	
	public void FirstUpdate( string populationNodeName, List<SoldierUnit> soldiersInNode, List<SpyUnit> spiesInNode)
	{
		SelectedSpies = 0;
		SelectedSoldiers = 0;

		_populationNodeSelectedText.text = populationNodeName;

		int numberOfSpies = spiesInNode.Count;
		int numberOfSoldiers = soldiersInNode.Count;

		_maximumSpyText.text = numberOfSpies.ToString();
		_maximumSoldierText.text = numberOfSoldiers.ToString();
				
		_spySelectionGameObject.SetActive(true);
		_soldierSelectionGameObject.SetActive(true);
		
		_spySelection.text = 0.ToString();
		_soldierSelection.text = 0.ToString();

        _recruitSpyButton.SetActive(true);
		_recruitSoldierButton.SetActive(true);

		_assassinateButtonGameObject.SetActive(true);
		_assassinateButton.interactable = false;

		_propagandaButtonGameObject.SetActive(true);
		_propagandaButton.interactable = false;

		_repressButtonGameObject.SetActive(true);
		_repressButton.interactable = false;
	}

    public void SubmitSelection()
	{
		int numberOfSpiesSelected = int.Parse(_spySelection.text);
		int maximumNumberOfSpiesPresent = int.Parse(_maximumSpyText.text);

		int numberOfSoldiersSelected = int.Parse(_soldierSelection.text);
		int maximumNumberOfSoldiersPresent = int.Parse(_maximumSoldierText.text);


		if (numberOfSpiesSelected < maximumNumberOfSpiesPresent)
		{
			SelectedSpies = numberOfSpiesSelected;
		}
		else
		{
			_spySelection.text = maximumNumberOfSpiesPresent.ToString();
			SelectedSpies = maximumNumberOfSpiesPresent;
		}


		if (numberOfSoldiersSelected < maximumNumberOfSoldiersPresent)
		{
			SelectedSoldiers = numberOfSoldiersSelected;
		}
		else
		{
			_soldierSelection.text = maximumNumberOfSoldiersPresent.ToString();
			SelectedSoldiers = maximumNumberOfSoldiersPresent;
		}

		_maximumSpyText.text = (maximumNumberOfSpiesPresent  - SelectedSpies).ToString();
		_maximumSoldierText.text = (maximumNumberOfSoldiersPresent  - SelectedSoldiers).ToString();
	}

    public void Update(int numberOfSoldiers, int numberOfSpies)
	{
		_maximumSpyText.text = numberOfSpies.ToString();
	}
	
	private void CheckSpyUnitSelectionIsValid(InputField input)
	{
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
			_assassinateButton.interactable = true;
			_propagandaButton.interactable = true;
			return;
		}	
	}

	private void CheckSoldierUnitSelectionIsValid(InputField input)
	{
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
			_repressButton.interactable = true;
			return;
		}
	}

	private char InputValidation(string text, int charIndex, char addedChar)
	{
		if (!char.IsNumber(addedChar))
		{
			return '\0';
		}
		else
		{
			return addedChar;
		}
	}
}
