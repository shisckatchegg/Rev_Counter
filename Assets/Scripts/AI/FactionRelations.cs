using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Events;

public class FactionRelations
{
	private Text _factionRelationsText;
	private int[][] _factionRelations;
	private int _numberOfFactions;

	public void PreInitialize (int numberOfFactions)
	{
		_factionRelationsText = GameObject.Find("FactionRelations").GetComponent<Text>();

		_numberOfFactions = numberOfFactions;
		_factionRelations = new int[_numberOfFactions][];
		for (int factionIndex = 0; factionIndex < _factionRelations.Length; factionIndex++)
		{
			_factionRelations[factionIndex] = new int[_numberOfFactions];
		}
	}

	public void Initialize()
	{
		EventManager.StartListening<PopulationNodeSelectedEventData>(EventNames.PopulationNodeSelected, OnPopulationNodeSelected);


		for (int factionIndex = 0; factionIndex < _factionRelations.Length; factionIndex++)
		{
			for (int foreignFactionIndex = 0; foreignFactionIndex < _factionRelations[factionIndex].Length; foreignFactionIndex++)
			{
				if (factionIndex == foreignFactionIndex)
				{
					_factionRelations[factionIndex][foreignFactionIndex] = -1;
				}
				else
				{
					_factionRelations[factionIndex][foreignFactionIndex] = 20;
				}
			}
		}
	}

	public void OnProcessTurn()
	{
	}

	public void DisplayAllFactionRelations()
	{
		_factionRelationsText.text = "\tFaction Relations: \n";
		for (int foreignFactionIndex = 0; foreignFactionIndex < _factionRelations[(int)Globals.PlayerFaction].Length; foreignFactionIndex++)
		{
			if (_factionRelations[(int)Globals.PlayerFaction][foreignFactionIndex] != -1)
			{
				_factionRelationsText.text += ((Globals.FactionNames)foreignFactionIndex).ToString() + ": " + _factionRelations[(int)Globals.PlayerFaction][foreignFactionIndex] + "\n";
			}
		}
	}

	private void OnPopulationNodeSelected(PopulationNodeSelectedEventData data)
	{
		DisplayFactionRelationsWithPlayer((Globals.FactionNames)data.Control);
	}

	public void DisplayFactionRelationsWithPlayer(Globals.FactionNames factionId)
	{
		int playerOnFaction = -1;
		int factionOnPlayer = -1;
		GetRelationsWithPlayer(factionId, out playerOnFaction, out factionOnPlayer);

		_factionRelationsText.text = "\tFaction Relations: \n" + "Our views on them: " + playerOnFaction + "\nTheir views on us: " + factionOnPlayer;
	}

	public void GetRelationsWithPlayer(Globals.FactionNames factionId, out int playerViewOnFaction, out int factionViewOnPlayer)
	{
		if (Globals.PlayerFaction != factionId)
		{
			playerViewOnFaction = _factionRelations[(int)Globals.PlayerFaction][(int)factionId];
			factionViewOnPlayer = _factionRelations[(int)factionId][(int)Globals.PlayerFaction];
		}
		else
		{
			playerViewOnFaction = -1;
			factionViewOnPlayer = -1;
		}
	}
}
