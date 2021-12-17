public class ShootingDamageUpgrade : GenericUpgrade
{
    public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
    {
        return shooter.damage < 3;
    }

    public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
    {
        shooter.damage += 0.5f;
    }
}