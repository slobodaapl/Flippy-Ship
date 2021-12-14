using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularSpeedUpgrade : GenericUpgrade
{
	public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
	{
		return ship.maxAngleSineSpeed < 1.5f;
	}

	public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
	{
		ship.maxAngleSineSpeed += 0.1f;
	}
}
