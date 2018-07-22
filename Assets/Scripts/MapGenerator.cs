using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	private Vector2 _currentMapSize;

	private GameObject [,] _backgroundObjects;

	private GameObject _groundColliderObject;

	private void Awake()
	{
		_currentMapSize = Globals.CurrentMapSize;

		GenerateTerrain();

		GenerateGroundCollider();

	}

	private void GenerateTerrain()
	{
		Sprite backgroundSprite = Resources.Load<Sprite>("Sprites/background");

		float backgroundWidth = backgroundSprite.bounds.size.x;
		float backgroundHeight = backgroundSprite.bounds.size.y;

		int numberOfBackgroundSpritesX = Mathf.CeilToInt(_currentMapSize.x / backgroundWidth);
		int numberOfBackgroundSpritesY = Mathf.CeilToInt(_currentMapSize.y / backgroundHeight);

		_backgroundObjects = new GameObject[numberOfBackgroundSpritesX, numberOfBackgroundSpritesY];

		Vector3 position = new Vector3(-_currentMapSize.x / 2, -_currentMapSize.y / 2, 5);
		Quaternion rotation = new Quaternion(0, 0, 0, 0);

		for (int x = 0; x < numberOfBackgroundSpritesX; x++)
		{
			for (int y = 0; y < numberOfBackgroundSpritesY; y++)
			{
				_backgroundObjects[x, y] = new GameObject("background");

				_backgroundObjects[x, y].AddComponent<SpriteRenderer>();
				_backgroundObjects[x, y].GetComponent<SpriteRenderer>().sprite = backgroundSprite;

				_backgroundObjects[x, y].transform.position = position;
				_backgroundObjects[x, y].transform.rotation = rotation;
				position.y += backgroundHeight;
			}

			position.y = -_currentMapSize.y / 2;

			position.x += backgroundWidth;
		}
	}

	private void GenerateGroundCollider()
	{
		_groundColliderObject = new GameObject("groundCollider");

		_groundColliderObject.AddComponent<BoxCollider2D>();

		switch (Globals.CurrentMapSizeType)
		{
			case Globals.MapSizes.Small:
				_groundColliderObject.GetComponent<BoxCollider2D>().size = Globals.SmallMapSize;
				break;
			case Globals.MapSizes.Medium:
				_groundColliderObject.GetComponent<BoxCollider2D>().size = Globals.MediumMapSize;
				break;
			case Globals.MapSizes.Large:
				_groundColliderObject.GetComponent<BoxCollider2D>().size = Globals.LargeMapSize;
				break;
		}


		_groundColliderObject.AddComponent<Deselect>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
