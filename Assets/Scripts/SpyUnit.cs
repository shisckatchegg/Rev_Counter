using UnityEngine;

public class SpyUnit : UnitBase
{
	public SpyUnit(PopulationNode location, Globals.FactionNames factionId)
		: base(location, factionId)
	{

	}

	// Use this for initialization
	protected override void Start ()
	{
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update();
    }

	public override void Movement(PopulationNode destination)
	{
		Location = destination;
	}

	public void Assassinate(GameObject target)
	{
		for(int unitIndex = 0; unitIndex < Location.PresentUnits.Count; unitIndex++)
		{
			if(!(Location.PresentUnits[unitIndex] is SoldierUnit))
			{
				//Random chance of killing the unit!
			}
		}
	}

	public void CounterEspionage()
	{

	}

	public void Infiltrate()
	{

	}

	public void PropagandaCampaign()
	{

	}

	public void BuildSpyNetwork()
	{

	}
}
