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
				Globals.CurrentMapSize = new Vector2(700, 700);
				Globals.CurrentMapSizeType = Globals.MapSizes.Small;
				break;
			case Globals.MapSizes.Medium:
				Globals.CurrentMapSize = new Vector2(1200, 1200);
				Globals.CurrentMapSizeType = Globals.MapSizes.Medium;
				break;
			case Globals.MapSizes.Large:
				Globals.CurrentMapSize = new Vector2(1800, 1800);
				Globals.CurrentMapSizeType = Globals.MapSizes.Large;
				break;
		}
	}
}
