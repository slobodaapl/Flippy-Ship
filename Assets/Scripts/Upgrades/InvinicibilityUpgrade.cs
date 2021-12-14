using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinicibilityUpgrade : GenericUpgrade
{
	public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
	{
		return ship.invincibilityDuration < 6;
	}

	public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
	{
		ship.invincibilityDuration += 0.5f;
	}
}
