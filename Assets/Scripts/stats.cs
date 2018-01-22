using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationStats
{
	public int Control;

	public int Support;

	public void SupportDecay()
	{
		if(Control > 60)
		{
			Support--;
		}
	}
}


public class stats : MonoBehaviour {

	private PopulationStats _stats;

	// Use this for initialization
	void Start () {
		_stats = new PopulationStats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
