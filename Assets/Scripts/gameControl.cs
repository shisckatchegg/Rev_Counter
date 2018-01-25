
public class GameControl
{
	private TurnControl _turnControl;

	private int _turnNumber;

	public void ProcessGame()
	{
		_turnControl.ProcessTurn();
		_turnNumber++;
	}


}

public class TurnControl
{
	public CityStatProcesser _cityStats;

	public UnitStatProcesser _unitStats;

	public PlayerStatProcesser _playerStats;

	public void ProcessTurn()
	{
		_cityStats.Update();

		_unitStats .Update();

		_playerStats.Update();
	}
}