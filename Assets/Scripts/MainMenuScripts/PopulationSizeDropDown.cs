using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationSizeDropDown : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPopulationSize(int choseSize)
	{
		switch ((Globals.PopulationSizes)choseSize)
		{
			case Globals.PopulationSizes.Small:
				Globals.CurrentPopulationSize = Globals.PopulationSizes.Small;
				break;
			case Globals.PopulationSizes.Medium:
				Globals.CurrentPopulationSize = Globals.PopulationSizes.Medium;
				break;
			case Globals.PopulationSizes.Large:
				Globals.CurrentPopulationSize = Globals.PopulationSizes.Large;
				break;
		}
	}
}
