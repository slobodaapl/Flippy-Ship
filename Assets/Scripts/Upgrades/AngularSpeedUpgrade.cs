public class AngularSpeedUpgrade : GenericUpgrade
{
    public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
    {
        return ship.maxAngleSineSpeed < 15;
    }

    public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
    {
        ship.maxAngleSineSpeed += 1;
    }
}