using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestoreUpgrade : GenericUpgrade
{
	public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
	{
		return ship.health < ship.maxHealth;
	}

	public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
	{
		ship.health = ship.maxHealth;
		ship.NotifyHealthObserver(false);
	}
}
