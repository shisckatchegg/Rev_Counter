using UnityEngine;

public class SpyUnit : UnitBase
{
	public int AssasinationChance = 60;     //Chance of performing successful assasination
	public float PropagandaEffectiveness = 0.03f;

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
		int randomAssassinationTarget = Random.Range(0, CurrentLocation.PresentSpies.Count - 1);

		if (Random.Range(0, 100) < AssasinationChance)
		{
			CurrentLocation.PresentSpies.RemoveAt(randomAssassinationTarget);
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
		float currentSupport = CurrentLocation.Stats.GetFactionSupport(Faction);
		CurrentLocation.Stats.SetFactionSupport(Faction, currentSupport + PropagandaEffectiveness);
	}

	public void BuildSpyNetwork()
	{

	}
}
