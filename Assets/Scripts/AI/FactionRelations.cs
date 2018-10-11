using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public enum RelationStatus
{
	Invalid = -1,
	Peace,
	War,
	Ally,
	Federation,
}

public class FactionRelations
{
	private static int[][] _factionRelations;
	private int _numberOfFactions;
	private static RelationStatus[][] _factionRelationStatuses;

	public void PreInitialize (int numberOfFactions)
	{
		_numberOfFactions = numberOfFactions;
		_factionRelations = new int[_numberOfFactions][];
		for (int factionIndex = 0; factionIndex < _factionRelations.Length; factionIndex++)
		{
			_factionRelations[factionIndex] = new int[_numberOfFactions];
		}

		_factionRelationStatuses = new RelationStatus[numberOfFactions][];
		for (int factionIndex = 0; factionIndex < _factionRelationStatuses.Length; factionIndex++)
		{
			_factionRelationStatuses[factionIndex] = new RelationStatus[_numberOfFactions];
		}
	}

	public void Initialize()
	{
		EventManager.StartListening<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, OnPlayerDiplomaticStatusChange);
		
		for (int factionIndex = 0; factionIndex < _factionRelations.Length; factionIndex++)
		{
			for (int foreignFactionIndex = 0; foreignFactionIndex < _factionRelations[factionIndex].Length; foreignFactionIndex++)
			{
				if (factionIndex == foreignFactionIndex)
				{
					_factionRelations[factionIndex][foreignFactionIndex] = -1;
					_factionRelationStatuses[factionIndex][foreignFactionIndex] = RelationStatus.Invalid;
				}
				else
				{
					_factionRelations[factionIndex][foreignFactionIndex] = 20;
					_factionRelationStatuses[factionIndex][foreignFactionIndex] = RelationStatus.Peace;
				}
			}
		}
	}

	public void OnProcessTurn()
	{
	}

	public static void GetRelationsWithPlayer(Globals.FactionNames factionId, out int playerViewOnFaction, out int factionViewOnPlayer)
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

	public static RelationStatus GetRelationStatusWithPlayer(Globals.FactionNames factionId)
	{
		if (Globals.PlayerFaction != factionId)
		{
			return _factionRelationStatuses[(int)Globals.PlayerFaction][(int)factionId];
		}
		else
		{
			return RelationStatus.Invalid;
		}
	}

	private void OnPlayerDiplomaticStatusChange(DiplomaticStatusData data)
	{
		_factionRelationStatuses[(int)data.Source][(int)data.Target] = data.DiplomaticStatus;
		_factionRelationStatuses[(int)data.Target][(int)data.Source] = data.DiplomaticStatus;
	}
}
