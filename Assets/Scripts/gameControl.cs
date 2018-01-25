﻿using UnityEngine;


public class GameControl : MonoBehaviour
{
	private TurnControl _turnControl = null;

	public int TurnNumber;

	private void Start()
	{
		_turnControl = new TurnControl();
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

	public TurnControl()
	{
		_cityStats = new CityStatProcesser();
		_unitStats = new UnitStatProcesser();
		_playerStats = new PlayerStatProcesser();
	}

	public void ProcessTurn()
	{
		_cityStats.Update();

		_unitStats.Update();

		_playerStats.Update();
	}
}