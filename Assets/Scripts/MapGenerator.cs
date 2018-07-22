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
		Sprite seaBackgroundSprite = Resources.Load<Sprite>("Sprites/seaBackground");

		float backgroundWidth = backgroundSprite.bounds.size.x;
		float backgroundHeight = backgroundSprite.bounds.size.y;

		int numberOfBackgroundSpritesX = Mathf.CeilToInt(_currentMapSize.x / backgroundWidth);
		int numberOfBackgroundSpritesY = Mathf.CeilToInt(_currentMapSize.y / backgroundHeight);

		numberOfBackgroundSpritesX += 4;
		numberOfBackgroundSpritesY += 4;
		
		_backgroundObjects = new GameObject[numberOfBackgroundSpritesX, numberOfBackgroundSpritesY];

		Vector3 position = new Vector3(-_currentMapSize.x / 2, -_currentMapSize.y / 2, 5);
		position.x -= backgroundWidth * 2;
		position.y -= backgroundHeight * 2;
		Quaternion rotation = new Quaternion(0, 0, 0, 0);

		for (int x = 0; x < numberOfBackgroundSpritesX; x++)
		{
			for (int y = 0; y < numberOfBackgroundSpritesY; y++)
			{
				_backgroundObjects[x, y] = new GameObject("background");

				_backgroundObjects[x, y].AddComponent<SpriteRenderer>();

				if ((x == 0 || x == 1 || x == numberOfBackgroundSpritesX - 2 || x == numberOfBackgroundSpritesX - 1)
				|| (y == 0 || y == 1 || y == 2 || y == numberOfBackgroundSpritesY - 2 || y == numberOfBackgroundSpritesY - 1))
				{
					_backgroundObjects[x, y].GetComponent<SpriteRenderer>().sprite = seaBackgroundSprite;
				}
				else
				{
					_backgroundObjects[x, y].GetComponent<SpriteRenderer>().sprite = backgroundSprite;
				}

				_backgroundObjects[x, y].transform.position = position;
				_backgroundObjects[x, y].transform.rotation = rotation;
				position.y += backgroundHeight;
			}

			position.y = -_currentMapSize.y / 2;
			position.y -= backgroundHeight * 2;

			position.x += backgroundWidth;
		}
	}

	private void GenerateGroundCollider()
	{
		_groundColliderObject = new GameObject("groundCollider");

		_groundColliderObject.AddComponent<BoxCollider2D>();

		Vector2 colliderSize;
		switch (Globals.CurrentMapSizeType)
		{
			case Globals.MapSizes.Small:
				colliderSize = Globals.SmallMapSize;
				break;
			case Globals.MapSizes.Medium:
				colliderSize = Globals.MediumMapSize;
				break;
			case Globals.MapSizes.Large:
				colliderSize = Globals.LargeMapSize;
				break;
			default:
				Debug.Log("Invalid map size!");
				colliderSize = new Vector2(200, 200);
				break;
		}

		colliderSize.x += 100;
		colliderSize.y += 100;

		_groundColliderObject.GetComponent<BoxCollider2D>().size = colliderSize;
		_groundColliderObject.AddComponent<Deselect>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
