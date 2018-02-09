using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
	public Globals.FactionNames Faction;

    public PopulationNode Location;

    public UnitBase(PopulationNode location, Globals.FactionNames factionNames)
	{
        Location = location;
		Faction = factionNames;
    }

    // Use this for initialization
    protected virtual void Start ()
	{

	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
	}

	public abstract void Movement(PopulationNode destination);

}
