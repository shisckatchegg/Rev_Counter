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

	public override void InitiateMovement(PopulationNode destination)
	{
		Destination = destination;
	}

	public override void ExecuteMovement()
	{
		if (Destination != null)
		{
			for (int spyIndex = 0; spyIndex < CurrentLocation.PresentSpies.Count; spyIndex++)
			{
				if(CurrentLocation.PresentSpies[spyIndex] == this)
				{
					CurrentLocation.PresentSpies.Remove(this);
				}
			}

			Destination.PresentSpies.Add(this);

			CurrentLocation = Destination;
			Destination = null;
		}
	}

	public void Assassinate(GameObject target)
	{
		for(int unitIndex = 0; unitIndex < CurrentLocation.PresentSpies.Count; unitIndex++)
		{
			//Random chance of killing the unit!
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
