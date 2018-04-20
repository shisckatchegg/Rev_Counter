using UnityEngine;


public class GameControl : MonoBehaviour
{
	private TurnControl _turnControl = null;

	public int TurnNumber;
	
	private void Awake()
	{
		_turnControl = new TurnControl();
		_turnControl.PreInitializeProcessers();
	}

	private void Start()
	{		
		_turnControl.InitializeProcessers();
	}

	private void Update()
	{
		
	}

	public void ProcessGame()
	{
		_turnControl.ProcessTurn();
		TurnNumber++;
	}
}

internal class TurnControl
{
	public CityStatProcesser _cityStats;

	public UnitStatProcesser _unitStats;

	public PlayerStatProcesser _playerStats;

	public DecisionMaker _decisionMaker;

	public TurnControl()
	{
		_cityStats = new CityStatProcesser();
		_unitStats = new UnitStatProcesser();
		_playerStats = new PlayerStatProcesser();
		_decisionMaker = new DecisionMaker();
	}

	public void PreInitializeProcessers()
	{
		_playerStats.PreInitialize();

		_decisionMaker.PreInitialize();
	}

	public void InitializeProcessers()
	{
		_cityStats.Initialize();
		_unitStats.Initialize(_playerStats.GameFactions);
		_playerStats.Initialize();

		_decisionMaker.Initialize(_playerStats.GameFactions);
	}

	public void ProcessTurn()
	{
		_decisionMaker.OnProcessTurn();

		_cityStats.OnProcessTurn();

		_unitStats.OnProcessTurn();

		_playerStats.OnProcessTurn();

	}
}
