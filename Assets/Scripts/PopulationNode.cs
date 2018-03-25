using UnityEngine;
using System.Collections.Generic;

public class PopulationNode : MonoBehaviour
{
	public PopulationNodeStats Stats;

	public List<SoldierUnit> PresentSoldiers;
	public List<SpyUnit> PresentSpies;

	public PopulationNodeSelection PopulationNodeSelection;

	private void Awake()
	{
		PopulationNodeSelection = GameObject.Find("SelectedPopulationNodeText").GetComponent<PopulationNodeSelection>();
	}

	// Use this for initialization
	void Start()
	{
        PresentSoldiers = new List<SoldierUnit>();
        PresentSpies = new List<SpyUnit>();

    }


    public void ProcessStats()
	{
		Stats.Update();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseDown()
	{
		PopulationNodeSelection.InitializeSelection(this);
	}

	private void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(1))
		{
			PopulationNodeSelection.InitiateMovement(this);
			Debug.Log("City destination selected to move unit: " + Stats.PopulationNodeName);
		}
	}
}
