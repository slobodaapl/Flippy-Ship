public class ShootingTargetsUpgrade : GenericUpgrade
{
    public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
    {
        return shooter.maxTargets < 3;
    }

    public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
    {
        shooter.maxTargets += 1;
    }
}