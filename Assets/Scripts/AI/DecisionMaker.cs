using UnityEngine;
using Events;

public class DecisionMaker
{
	public Faction[] AIFactions;

	private int _currentFaction;

	private PopulationNode[] _populationNodes;

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


	}

	public void OnProcessTurn()
	{
		for(_currentFaction = 0; _currentFaction < AIFactions.Length; _currentFaction++)
		{
			for (int populationNodeIndex = 0; populationNodeIndex < _populationNodes.Length; populationNodeIndex++)
			{
				RecruitSpy(_populationNodes[populationNodeIndex]);
			}
		}
	}

	private void EvaluateFunds()
	{

	}

	private void EvaluateUnits()
	{

	}

	private void EvaluatePopulationNodes()
	{

	}

	private Faction GetCurrentAIFaction()
	{
		return AIFactions[_currentFaction];
	}

	private void RecruitSpy(PopulationNode populationNode)
	{
		SpyUnit newSpy = new SpyUnit(populationNode, GetCurrentAIFaction().FactionId);
		populationNode.PresentSpies.Add(newSpy);
		EventManager.TriggerEvent(EventNames.SpyRecruited, new UnitRecruitmentData() { NewlyRecruitedUnit = newSpy });
	}
}
