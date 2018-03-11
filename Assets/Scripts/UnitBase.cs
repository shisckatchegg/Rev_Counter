using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
	public Globals.FactionNames Faction;

    public PopulationNode CurrentLocation;

	public PopulationNode Destination;

    public UnitBase(PopulationNode location, Globals.FactionNames factionNames)
	{
        CurrentLocation = location;
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

	public abstract void InitiateMovement(PopulationNode destination);

	public abstract void ExecuteMovement();

}
