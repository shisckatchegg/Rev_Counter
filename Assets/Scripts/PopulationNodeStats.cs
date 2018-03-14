using System.Collections.Generic;
using UnityEngine;

public enum CityType
{
	Invalid = -1,
	Village = 0,
	City = 1,

	MAX_TYPES,
}


[System.Serializable]
public class PopulationNodeStats
{
	public string PopulationNodeName;

	public int Control;							

	[SerializeField]
	private List<float> _factionsSupport;			//Sum of all factions support must be 1

	public List<float> FactionsSupport
	{
		get { return _factionsSupport; }
		set { _factionsSupport = value; }
	}

	public int Population;

	public int RecruitmentSlots;

	public CityType Type;

	private const float SUPPORT_DECAY_THRESHOLD = 0.6f;
	private const float SUPPORT_DECAY_AMOUNT = 0.05f;

	public PopulationNodeStats(string name, int control, List<float> factionsSupport, int population, CityType type)
	{
		PopulationNodeName = name;
		Control = control;
		FactionsSupport = factionsSupport;
		Population = population;
		Type = type;
	}

	public void Update()
	{
		SupportDecay();
	}

	private void SupportDecay()
	{
		if (FactionsSupport == null)
		{
			Debug.Assert(FactionsSupport == null, "Population node: " + PopulationNodeName + " faction support list is null!");
		}

		Stack<int> factionIndecesUnderSafeSupportLimit = new Stack<int>();
		
		//All factions over a certain support threshold have it decay
		for(int factionSupportIndex = 0; factionSupportIndex < FactionsSupport.Count; factionSupportIndex++)
		{
			if(FactionsSupport[factionSupportIndex] > SUPPORT_DECAY_THRESHOLD)
			{
				FactionsSupport[factionSupportIndex] -= SUPPORT_DECAY_AMOUNT; 
			}
			else
			{
				factionIndecesUnderSafeSupportLimit.Push(factionSupportIndex);
			}
		}


		float totalSupport = 1.0f;
		float currentTotalSupport = 0.0f;

		//All support must sum 1. After the decrease, share the difference between the lowest support factions
		for (int factionSupportIndex = 0; factionSupportIndex < FactionsSupport.Count; factionSupportIndex++)
		{
			currentTotalSupport += FactionsSupport[factionSupportIndex];
		}

		float unassignedSupport = totalSupport - currentTotalSupport;
		float supportPerFaction = unassignedSupport / factionIndecesUnderSafeSupportLimit.Count;

		for (int stackElement = 0; stackElement < factionIndecesUnderSafeSupportLimit.Count; stackElement++)
		{
			int factionIndex = factionIndecesUnderSafeSupportLimit.Pop();

			FactionsSupport[factionIndex] += supportPerFaction;
			unassignedSupport -= supportPerFaction;
		}

		if(unassignedSupport > 0.0f)
		{
			float smallestSupport = float.MaxValue;
			int smallestSupportIndex = -1;
			for (int factionIndex = 0; factionIndex < FactionsSupport.Count; factionIndex++)
			{
				if(FactionsSupport[factionIndex] < smallestSupport)
				{
					smallestSupportIndex = factionIndex;
				}
			}

			if(smallestSupportIndex != -1)
			{
				FactionsSupport[smallestSupportIndex] += unassignedSupport;
				unassignedSupport = 0;
			}
		}
	}

	public float GetFactionSupport(Globals.FactionNames faction)
	{
		return _factionsSupport[(int)faction];
	}

	public void SetFactionSupport(Globals.FactionNames faction, float newSupport)
	{
		_factionsSupport[(int)faction] = newSupport;
	}
}
