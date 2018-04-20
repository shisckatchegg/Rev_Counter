using UnityEngine.UI;
using UnityEngine;

public class FactionDisplay
{
	private Text _factionDataDisplay;
	
	public void InitializeTextDisplay()
	{
		_factionDataDisplay = GameObject.Find("FactionInfoDisplay").GetComponent<Text>();
	}

	public void FirstUpdate(Globals.FactionNames factionId, int numberOfControlledCities, int numberOfSpies, int numberOfMilitary)
	{
		_factionDataDisplay.text = "Faction: " + factionId
			+ "\nCities under control: " + numberOfControlledCities
			+ "\nNumber of active spies: " + numberOfSpies
			+ "\nNumber of active squads: " + numberOfMilitary;
	}

	public void Update(Globals.FactionNames factionId, int numberOfControlledCities, int numberOfSpies, int numberOfMilitary)
	{
		_factionDataDisplay.text = "Faction: " + factionId
			+ "\nCities under control: " + numberOfControlledCities
			+ "\nNumber of active spies: " + numberOfSpies
			+ "\nNumber of active squads: " + numberOfMilitary;
	}
}
