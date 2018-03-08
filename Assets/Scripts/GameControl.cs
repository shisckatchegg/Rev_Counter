using UnityEngine;


public class GameControl : MonoBehaviour
{
	private TurnControl _turnControl = null;

	public int TurnNumber;

    public PopulationNodeSelection selection;
    public Globals.FactionNames FactionId;

    private void Start()
	{
		_turnControl = new TurnControl();
		_turnControl.InitializeProcessers();
        //selection = GameObject.Find("SelectedPopulationNode");

        //GetComponent<PopulationNodeSelection>();
    }

	public void ProcessGame()
	{
		_turnControl.ProcessTurn();
		TurnNumber++;
	}

    public void RecruitSpy()
    {
        Debug.Log("spy recruitment");

        selection._populationNode.PresentUnits.Add(new SpyUnit(selection._populationNode, FactionId));

        Debug.Log(selection._populationNode.PresentUnits.Count);
    }
}

internal class TurnControl
{
	public CityStatProcesser _cityStats;

	public UnitStatProcesser _unitStats;

	public PlayerStatProcesser _playerStats;

	public TurnControl()
	{
		_cityStats = new CityStatProcesser();
		_unitStats = new UnitStatProcesser();
		_playerStats = new PlayerStatProcesser();
	}

	public void InitializeProcessers()
	{
		_cityStats.Initialize();
		_unitStats.Initialize();
		_playerStats.Initialize();
	}

	public void ProcessTurn()
	{
		_cityStats.Update();

		_unitStats.Update();

		_playerStats.Update();
	}
}
