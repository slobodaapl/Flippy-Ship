using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSpeedUpgrade : GenericUpgrade
{
    public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
    {
        return ship.turnRateDegreesSecond < 90;
    }

    public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
    {
        ship.turnRateDegreesSecond += 5;
    }
}
