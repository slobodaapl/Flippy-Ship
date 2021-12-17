using UnityEngine;

public class BlueEnemyShip : BaseShip
{
    protected override void FixedUpdate() // Blue enemy ship shoots 2 zigzagging destructible large projectiles
    {
        base.FixedUpdate();

        if (!isVisible || isLeaving) return;

        if (currentCooldown <= 0)
        {
            currentCooldown = shotCooldown;

            var pos = transform.position;
            var shotOne = Instantiate(projectile, pos, Quaternion.identity)
                .GetComponent<BlueProjectile>();
            var shotTwo = Instantiate(projectile, pos, Quaternion.identity)
                .GetComponent<BlueProjectile>();

            shotOne.InitOffset(xOffset);
            shotTwo.InitOffset(xOffset);

            shotTwo.TriggerSineOffset();
        }

        currentCooldown -= Time.fixedDeltaTime;
    }
}