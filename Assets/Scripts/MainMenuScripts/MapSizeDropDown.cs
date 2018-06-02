using UnityEngine;

public class MapSizeDropDown : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMapSize(int choseSize)
	{
		switch ((Globals.MapSizes)choseSize)
		{
			case Globals.MapSizes.Small:
				Globals.CurrentMapSize = new Vector2(2048, 2048);
				break;
			case Globals.MapSizes.Medium:
				Globals.CurrentMapSize = new Vector2(4096, 4096);
				break;
			case Globals.MapSizes.Large:
				Globals.CurrentMapSize = new Vector2(8192, 8192);
				break;
		}
	}
}
