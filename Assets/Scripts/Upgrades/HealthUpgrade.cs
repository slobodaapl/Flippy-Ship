public class HealthUpgrade : GenericUpgrade
{
    public override bool CheckValid(PlayerShip ship, PlayerShooter shooter)
    {
        return true;
    }

    public override void ApplyUpgrade(PlayerShip ship, PlayerShooter shooter)
    {
        ship.maxHealth += 1;
        ship.health += 1;
        ship.NotifyHealthObserver(false);
    }
}