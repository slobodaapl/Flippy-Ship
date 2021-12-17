public class ShootingLengthUpgrade : GenericUpgrade
{
    public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
    {
        return shooter.GetColliderRadius() < 20;
    }

    public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
    {
        shooter.OffsetColliderRadius(0.5f);
    }
}