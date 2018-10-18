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

public enum FactionRelationElement
{
	Invalid = -1,
	Proximity,
	CurrentStatus,
	Strength,
	OccupiedUs,
	AttackedUs,
}

public class FactionRelationElementData
{
	public int Value;
	public int TimeToLive;
	public FactionRelationElement FactionRelationElementId;
}

public  class FactionRelationData
{
	public List<FactionRelationElementData> RelationElements;

	public RelationStatus FactionRelationStatus;

	public FactionRelationData()
	{
		RelationElements = new List<FactionRelationElementData>();
	}

	public void UpdateFactionRelationData()
	{
		ProcessTimeToLive();
		CalculateFactionRelationElementValue();
	}

	private void ProcessTimeToLive()
	{
		RelationElements.ForEach(x => x.TimeToLive--);
	}

	private void CalculateFactionRelationElementValue()
	{
		for(int factionRelationElementDataIndex = 0; factionRelationElementDataIndex < RelationElements.Count; factionRelationElementDataIndex++)
		{
			switch(RelationElements[factionRelationElementDataIndex].FactionRelationElementId)
			{
				case FactionRelationElement.Proximity:
					RelationElements[factionRelationElementDataIndex].Value = 10;
					break;
				case FactionRelationElement.CurrentStatus:
					RelationElements[factionRelationElementDataIndex].Value = GetCurrentRelationStatusElementValue();
					break;
				case FactionRelationElement.Strength:
					RelationElements[factionRelationElementDataIndex].Value = 10;
					break;
				case FactionRelationElement.OccupiedUs:
					RelationElements[factionRelationElementDataIndex].Value = 10;
					break;
				case FactionRelationElement.AttackedUs:
					RelationElements[factionRelationElementDataIndex].Value = 10;
					break;
			}
		}
	}

	private int GetCurrentRelationStatusElementValue()
	{
		switch(FactionRelationStatus)
		{
			case RelationStatus.Ally:
				return 20;
			case RelationStatus.Federation:
				return 30;
			case RelationStatus.Peace:
				return 0;
			case RelationStatus.War:
				return -20;
			default:
				Debug.Log("GetCurrentRelationStatusElementValue - Invalid FactionRelationStatus: " + FactionRelationStatus.ToString());
				break;
		}

		return -1;
	}

	public int GetRelationsTotal()
	{
		int total = 0;
		RelationElements.ForEach(x => total += x.Value);
		return total;
	}
}

public class FactionRelations
{
	private static FactionRelationData[][] _factionRelations;
	private int _numberOfFactions;
	private static RelationStatus[][] _factionRelationStatuses;

	public void PreInitialize (int numberOfFactions)
	{
		_numberOfFactions = numberOfFactions;
		_factionRelations = new FactionRelationData[_numberOfFactions][];
		for (int factionIndex = 0; factionIndex < _factionRelations.Length; factionIndex++)
		{
			_factionRelations[factionIndex] = new FactionRelationData[_numberOfFactions];
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
				_factionRelations[factionIndex][foreignFactionIndex] = new FactionRelationData();
				if (factionIndex == foreignFactionIndex)
				{
					_factionRelations[factionIndex][foreignFactionIndex].RelationElements = new List<FactionRelationElementData>();
					_factionRelations[factionIndex][foreignFactionIndex].RelationElements.Add(new FactionRelationElementData() { Value = -1, FactionRelationElementId = FactionRelationElement.Invalid });
					_factionRelationStatuses[factionIndex][foreignFactionIndex] = RelationStatus.Invalid;
				}
				else
				{
					_factionRelations[factionIndex][foreignFactionIndex].RelationElements.Add(new FactionRelationElementData() { Value = 10, FactionRelationElementId = FactionRelationElement.Proximity });
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
			playerViewOnFaction = _factionRelations[(int)Globals.PlayerFaction][(int)factionId].GetRelationsTotal();
			factionViewOnPlayer = _factionRelations[(int)factionId][(int)Globals.PlayerFaction].GetRelationsTotal();
		}
		else
		{
			playerViewOnFaction = -1;
			factionViewOnPlayer = -1;
		}
	}

	public static List<FactionRelationElementData> GetFactionRelationElementData(Globals.FactionNames factionId)
	{
		return _factionRelations[(int)Globals.PlayerFaction][(int)factionId].RelationElements;
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
