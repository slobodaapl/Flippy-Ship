using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAngleUpgrade : GenericUpgrade
{
	public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
	{
		return shooter.maxAngle < 25;
	}

	public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
	{
		shooter.maxAngle += 2.5f;
	}
}
