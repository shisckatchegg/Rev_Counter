using UnityEngine;

public class SoldierUnit : UnitBase
{
	public SoldierUnit(PopulationNode location, Globals.FactionNames factionId)
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

	}
}
