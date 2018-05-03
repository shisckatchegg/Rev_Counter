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

    public int GetIncome() {
        if (CityType.City == Type)
        {
            return 100;
        }
        else
        {
            return 50;
        }
    }

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
		return FactionsSupport[(int)faction];
	}

	public void SetFactionSupport(Globals.FactionNames faction, float newSupport)
	{
		float originalSupport = FactionsSupport[(int)faction];
		FactionsSupport[(int)faction] = newSupport;
		float supportDifference = newSupport - originalSupport;
		if(supportDifference > 0)
		{
			if(FactionsSupport[(int)faction] > 1.0f)
			{
				FactionsSupport[(int)faction] = 1.0f;
				supportDifference = 1.0f - originalSupport;
			}


			float supportPerFaction = supportDifference / (_factionsSupport.Count - 1);

			float remainingSupport = 0.0f;

			for (int factionSupportIndex = 0; factionSupportIndex < FactionsSupport.Count; factionSupportIndex++)
			{
				if (factionSupportIndex != (int)faction)
				{
					FactionsSupport[factionSupportIndex] -= supportPerFaction;

					if(FactionsSupport[factionSupportIndex] < 0)
					{
						remainingSupport = -FactionsSupport[factionSupportIndex];
						FactionsSupport[factionSupportIndex] = 0.0f;
					}
				}
			}

			if (remainingSupport > 0.0f)
			{
				float biggestSupport = float.MinValue;
				int biggestSupportIndex = -1;
				for (int factionIndex = 0; factionIndex < FactionsSupport.Count; factionIndex++)
				{
					if (FactionsSupport[factionIndex] < biggestSupport && factionIndex != (int)faction)
					{
						biggestSupportIndex = factionIndex;
					}
				}

				if (biggestSupportIndex != -1)
				{
					FactionsSupport[biggestSupportIndex] -= remainingSupport;
					remainingSupport = 0;
				}
			}
		}
	}
}
