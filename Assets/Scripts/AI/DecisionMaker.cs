using UnityEngine;
using System.Collections.Generic;
using Events;

public class DecisionMaker
{
	public Faction[] AIFactions;

	private int _currentFaction;

	private PopulationNode[] _populationNodes;

	private int[][] _populationNodeImportances;

	private int[][] _factionRelations;

	public void PreInitialize()
	{
		GameObject[] populationNodeGameObjects = GameObject.FindGameObjectsWithTag("PopulationNode");

		_populationNodes = new PopulationNode[populationNodeGameObjects.Length];
		for (int populationNodeIndex = 0; populationNodeIndex < populationNodeGameObjects.Length; populationNodeIndex++)
		{
			_populationNodes[populationNodeIndex] = populationNodeGameObjects[populationNodeIndex].GetComponent<PopulationNode>();
		}
	}

	public void Initialize(Faction[] factions)
	{
		_currentFaction = 0;

		//excluding the player faction
		AIFactions = new Faction[factions.Length - 1];

		int AIFactionsIndex = 0;
		for (int factionIndex = 0; factionIndex < factions.Length; factionIndex++)
		{
			if (factions[factionIndex].FactionId != Globals.PlayerFaction)
			{
				AIFactions[AIFactionsIndex] = factions[factionIndex];
				AIFactionsIndex++;
			}
		}

		_factionRelations = new int[AIFactions.Length + 1][];
		for (int factionIndex = 0; factionIndex < _factionRelations.Length; factionIndex++)
		{
			_factionRelations[factionIndex] = new int[AIFactions.Length];
		}

		_populationNodeImportances = new int[AIFactions.Length][];
		for (int populationNodeIndex = 0; populationNodeIndex < _populationNodeImportances.Length; populationNodeIndex++)
		{
			_populationNodeImportances[populationNodeIndex] = new int[_populationNodes.Length];
		}
	}

	public void OnProcessTurn()
	{
		EvaluatePopulationNodeImportances();

		for (_currentFaction = 0; _currentFaction < AIFactions.Length; _currentFaction++)
		{
			for (int populationNodeIndex = 0; populationNodeIndex < _populationNodes.Length; populationNodeIndex++)
			{
				if (_populationNodeImportances[_currentFaction][populationNodeIndex] > 15)
				{
					RecruitSpy(_populationNodes[populationNodeIndex]);
				}
			}
		}

		for (_currentFaction = 0; _currentFaction < AIFactions.Length; _currentFaction++)
		{
			UnitMovementOrders(_currentFaction);
		}
	}
	private void EvaluateFunds()
	{

	}

	private void EvaluateUnits()
	{

	}

	/// <summary>
	/// How important a node is for a faction. This can be based on:
	/// Income
	/// Population
	/// Support
	/// Military presence
	/// Ranges between 0 - 100
	/// </summary>
	private void EvaluatePopulationNodeImportances()
	{
		for(int factionIndex = 0; factionIndex < _populationNodeImportances.Length; factionIndex++)
		{
			for(int populationNodeIndex = 0; populationNodeIndex < _populationNodeImportances[factionIndex].Length; populationNodeIndex++)
			{
				int presentSoldiers = _populationNodes[populationNodeIndex].PresentSoldiers.Count;
				int presentSpies = _populationNodes[populationNodeIndex].PresentSpies.Count;
				int cityTypeValue = _populationNodes[populationNodeIndex].Stats.Type == CityType.City ? 1 : 0;
				float factionSupport = _populationNodes[populationNodeIndex].Stats.GetFactionSupport(AIFactions[factionIndex].FactionId);
				int factionControlValue = (_populationNodes[populationNodeIndex].Stats.Control == (int) AIFactions[factionIndex].FactionId) ? 1 : 0;

				cityTypeValue *= 10;
				factionControlValue *= 20;

				int factionSupportValue = 0;
				if(factionSupport > 0.20f && factionSupport <= 0.60f)
				{
					factionSupportValue = 10;
				}
				else if(factionSupport > 0.60f)
				{
					factionSupportValue = 30;
				}

				int presentSoldierValue = 0;
				if (presentSoldiers > 10 && presentSoldiers <= 50)
				{
					presentSoldierValue = 5;
				}
				else if (presentSoldiers > 50)
				{
					presentSoldierValue = 10;
				}

				//presentSoldierValue *= 2;

				int presentSpyValue = 0;
				if (presentSpies > 10 && presentSpies <= 50)
				{
					presentSpyValue = 10;
				}
				else if (presentSpies > 50)
				{
					presentSpyValue = 30;
				}

				int maximumPresentSoldierValue = 10;
				int maximumPresentSpyValue = 30;
				int maximumCityTypeValue = 10;
				int maximumFactionSupportValue = 30;
				int maximumFactionControlValue = 20;

				float underOne = ((float) (presentSoldierValue + presentSpyValue + cityTypeValue + factionSupportValue + factionControlValue)) / ((float) (maximumPresentSpyValue + maximumPresentSoldierValue + maximumCityTypeValue + maximumFactionSupportValue + maximumFactionControlValue));

				int populationNodeValue = (int)(underOne * 100);

				Debug.Log("Importance value of population node " + _populationNodes[populationNodeIndex].Stats.PopulationNodeName + " for faction " + AIFactions[factionIndex].FactionId.ToString() + " is: " + populationNodeValue);

				_populationNodeImportances[factionIndex][populationNodeIndex] = populationNodeValue;
			}
		}
	}

	private Faction GetCurrentAIFaction()
	{
		return AIFactions[_currentFaction];
	}

	private void RecruitSpy(PopulationNode populationNode)
	{
		Debug.Log("Spy recruitment in " + populationNode.Stats.PopulationNodeName + " for faction " + GetCurrentAIFaction().FactionId.ToString());

		populationNode.RecruitSpy(GetCurrentAIFaction().FactionId);
	}

	private void RecruitMilitary(PopulationNode populationNode)
	{

	}

	private void EvaluateSpyPresence()
	{

	}

	private void UnitMovementOrders(int factionIndex)
	{
		SpyMovementOrders(factionIndex);

		MilitaryMovementOrder(factionIndex);
	}

	private void SpyMovementOrders(int factionIndex)
	{
		int highestImportanceNode = -1;
		int lowestImportanceNode = 200;	//maximum importance
		for(int importanceIndex = 0; importanceIndex < _populationNodeImportances[factionIndex].Length; importanceIndex++)
		{
			if(highestImportanceNode < _populationNodeImportances[factionIndex][importanceIndex])
			{
				highestImportanceNode = importanceIndex;
			}

			if(lowestImportanceNode > _populationNodeImportances[factionIndex][importanceIndex])
			{
				lowestImportanceNode = importanceIndex;
			}
		}

		List<SpyUnit> lowImportancePopulationNodeSpies = _populationNodes[lowestImportanceNode].GetFactionSpies(GetCurrentAIFaction().FactionId);

		if (lowImportancePopulationNodeSpies.Count > 0)
		{
			foreach (var spy in lowImportancePopulationNodeSpies)
			{
				if (!spy.IsSpyBusy())
				{
					spy.InitiateMovement(_populationNodes[highestImportanceNode]);

					Debug.Log("Moving spy from " + _populationNodes[lowestImportanceNode].Stats.PopulationNodeName + " to " + _populationNodes[highestImportanceNode].Stats.PopulationNodeName);
				}
			}
		}
	}

	private void MilitaryMovementOrder(int factionIndex)
	{

	}
}
