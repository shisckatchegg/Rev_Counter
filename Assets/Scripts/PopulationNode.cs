using UnityEngine;
using System.Collections.Generic;

public class PopulationNode : MonoBehaviour
{
	public PopulationNodeStats Stats;

	public List<SoldierUnit> PresentSoldiers;
	public List<SpyUnit> PresentSpies;

	//TODO: might be a good idea to have a single population node selection and have it reassign population node data when one is clicked
	public PopulationNodeSelection _populationNodeSelection;

	private void Awake()
	{
		_populationNodeSelection = GameObject.Find("SelectedPopulationNodeText").GetComponent<PopulationNodeSelection>();
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
		_populationNodeSelection.InitializeSelection(this);
	}

	private void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(1))
		{
			_populationNodeSelection.InitiateMovement(this);
			Debug.Log("City destination selected to move unit: " + Stats.PopulationNodeName);
		}
	}
}
