using UnityEngine;
using UnityEngine.UI;

public class SelectionDisplay
{
	private Text _selectionText; 
	

	public void Initialization()
	{
		_selectionText = GameObject.Find("SelectedPopulationNode").GetComponent<Text>();
	}
	
	public void FirstUpdate ()
	{
		//_selectionText.text =
	}

	public void Update()
	{

	}
}
