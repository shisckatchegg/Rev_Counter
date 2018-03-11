using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectionDisplay
{
	private Text _selectionText;

	private GameObject _spySelectionGameObject;

	private Text _maximumSpyText;

	private InputField _spySelection;

	public int SelectedSpies;

	public void PreInitialization()
	{
		_selectionText = GameObject.Find("SelectedPopulationNode").GetComponent<Text>();
		_spySelectionGameObject = GameObject.Find("SpyCounter");
		_maximumSpyText = GameObject.Find("AmountOfSpiesPresent").GetComponent<Text>();
		_spySelection = _spySelectionGameObject.GetComponentInChildren<InputField>();
		_spySelection.onValidateInput = InputValidation;
		_spySelection.onEndEdit.AddListener(delegate { CheckSpyUnitSelectionIsValid(_spySelection); } );
	}

	public void Initialization()
	{
		_spySelectionGameObject.SetActive(false);
	}
	
	public void FirstUpdate( string populationNodeName, List<SoldierUnit> soldiersInNode, List<SpyUnit> spiesInNode)
	{
		SelectedSpies = 0;

		_selectionText.text = populationNodeName;

		int numberOfSpies = spiesInNode.Count;
		int numberOfSoldiers = soldiersInNode.Count;

		_maximumSpyText.text = numberOfSpies.ToString();
				
		_spySelectionGameObject.SetActive(true);
		_spySelection.text = 0.ToString();
	}

	public void SubmitSelection()
	{
		int numberOfSpiesSelected = int.Parse(_spySelection.text);
		int maximumNumberOfSpiesPresent = int.Parse(_maximumSpyText.text);
		

		if (numberOfSpiesSelected < maximumNumberOfSpiesPresent)
		{
			SelectedSpies = numberOfSpiesSelected;
		}
		else
		{
			_spySelection.text = maximumNumberOfSpiesPresent.ToString();
			SelectedSpies = maximumNumberOfSpiesPresent;
		}

		_maximumSpyText.text = (maximumNumberOfSpiesPresent  - SelectedSpies).ToString();
	}

    public void Update(int numberOfSoldiers, int numberOfSpies)
	{
		_maximumSpyText.text = numberOfSpies.ToString();
	}
	
	private void CheckSpyUnitSelectionIsValid(InputField input)
	{
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
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
