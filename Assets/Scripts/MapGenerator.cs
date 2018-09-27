using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	private Vector2 _currentMapSize;

	private GameObject [,] _backgroundObjects;

	private GameObject _groundColliderObject;

	public GameObject [] PopulationNodeGameObjects;

	public GameObject CityPrefab;
	public GameObject VillagePrefab;

	private void Awake()
	{
		_currentMapSize = Globals.CurrentMapSize;

		GenerateTerrain();

		GenerateGroundCollider();

		GeneratePopulation();
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

		GameObject terrain = new GameObject("terrain");

		for (int x = 0; x < numberOfBackgroundSpritesX; x++)
		{
			for (int y = 0; y < numberOfBackgroundSpritesY; y++)
			{
				_backgroundObjects[x, y] = new GameObject("background");
				_backgroundObjects[x, y].transform.parent = terrain.transform;

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
		_groundColliderObject.transform.position = new Vector3(0, 0, 5);

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

	private void GeneratePopulation()
	{
		int numberOfPopulationNodes = 30;
		switch(Globals.CurrentPopulationSize)
		{
			case Globals.PopulationSizes.Small:
				numberOfPopulationNodes = 30;
				break;
			case Globals.PopulationSizes.Medium:
				numberOfPopulationNodes = 50;
				break;
			case Globals.PopulationSizes.Large:
				numberOfPopulationNodes = 70;
				break;
			default:
				Debug.Log("Invalid population size!");
				numberOfPopulationNodes = 30;
				break;
		}

		PopulationNodeGameObjects = new GameObject[numberOfPopulationNodes];

		Vector2 safeMapSize = Globals.CurrentMapSize;
		safeMapSize.x *= 0.75f;
		safeMapSize.y *= 0.75f;

		Vector3 position = new Vector3( safeMapSize.x / 2, safeMapSize.y / 2, 0);
		Quaternion rotation = new Quaternion(0, 0, 0, 0);

		GameObject populationNodes = new GameObject("populationNodes");

		for (int populationNodeIndex = 0; populationNodeIndex < numberOfPopulationNodes; populationNodeIndex++)
		{
			float chanceOfVillageType = Random.Range(0f, 1f);

			GameObject populationNodeType = chanceOfVillageType < 0.6f ? VillagePrefab : CityPrefab;

			PopulationNodeGameObjects[populationNodeIndex] = Instantiate(populationNodeType, position, rotation);

			PopulationNodeGameObjects[populationNodeIndex].transform.parent = populationNodes.transform;

			PopulationNode populationNode = PopulationNodeGameObjects[populationNodeIndex].GetComponent<PopulationNode>();

			populationNode.Stats.PopulationNodeName = GetPopulationNodeName(Random.Range(0, 11));
			populationNode.Stats.Control = Random.Range(0, Globals.NumberOfFactions);
			populationNode.Stats.Population = Random.Range(525, 39550);

			populationNode.Stats.SetFactionSupport((Globals.FactionNames)0, Random.Range(0f, 1f));
			populationNode.Stats.SetFactionSupport((Globals.FactionNames)1, Random.Range(0f, 1f));
			populationNode.Stats.SetFactionSupport((Globals.FactionNames)2, Random.Range(0f, 1f));

			position.x = Random.Range(0, safeMapSize.x);
			position.x -= safeMapSize.x / 2;
			position.y = Random.Range(0, safeMapSize.y);
			position.y -= safeMapSize.y / 2;
		}
	}

	private string GetPopulationNodeName(int populationNodeIndex)
	{
		switch(populationNodeIndex)
		{
			case 0:		return "Kakariko";
			case 1:		return "Lilibaum";
			case 2:		return "Illia";
			case 3:		return "Renania";
			case 4:		return "Morcu";
			case 5:		return "Kivy";
			case 6:		return "Lenardo";
			case 7:		return "Petardo";
			case 8:		return "Peterdo";
			case 9:		return "Katerinar";
			case 10:	return "Arominodar";
			case 11:	return "ShipKilled";
		}

		return "";
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
