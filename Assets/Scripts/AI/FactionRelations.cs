using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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

		RelationsDisplay();
	}

	public void OnProcessTurn ()
	{
		RelationsDisplay();
	}

	public void RelationsDisplay()
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
}
