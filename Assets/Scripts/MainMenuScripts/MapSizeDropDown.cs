using UnityEngine;

public class MapSizeDropDown : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void SetMapSize(int choseSize)
	{
		switch ((Globals.MapSizes)choseSize)
		{
			case Globals.MapSizes.Small:
				Globals.CurrentMapSize = Globals.SmallMapSize;
				Globals.CurrentMapSizeType = Globals.MapSizes.Small;
				break;
			case Globals.MapSizes.Medium:
				Globals.CurrentMapSize = Globals.MediumMapSize;
				Globals.CurrentMapSizeType = Globals.MapSizes.Medium;
				break;
			case Globals.MapSizes.Large:
				Globals.CurrentMapSize = Globals.LargeMapSize;
				Globals.CurrentMapSizeType = Globals.MapSizes.Large;
				break;
		}
	}
}
