using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAngleUpgrade : GenericUpgrade
{
	public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
	{
		return ship.maxTurnAngle < 60;
	}

	public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
	{
		ship.maxTurnAngle += 2.5f;
	}
}
