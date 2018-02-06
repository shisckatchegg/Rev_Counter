using UnityEngine;

public class UnitBase : MonoBehaviour
{
	public Globals.FactionNames Faction;

    public GameObject Location;

    public UnitBase(GameObject location)
	{
        Location = location;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
}
