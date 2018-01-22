using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl
{

}

public class TurnControl
{
	public CityStatProcesser _cityStats;

	public UnitStatProcesser _unitStats;

	public PlayerStatPRocesser _playerStats;

	public void RefreshTurn()
	{
		_cityStats.Update();

		_unitStats .Update();

		_playerStats.Update();
	}
}
