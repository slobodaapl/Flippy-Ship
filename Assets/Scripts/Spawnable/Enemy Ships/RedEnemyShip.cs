using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedEnemyShip : BaseShip
{
    public float angleSpread = 5; // spread of projectile cascades

    public int
        cascadeCount =
            1; // 0 means only one central shot, 1 means central + 2 on each side, 2 is 1 + 2 then again 2.. etc

    private List<float> projectileAngles;

    protected override void Start() // Initialize the projectile angles
    {
        base.Start();

        projectileAngles = new List<float> { 0 };

        for (var i = 1; i <= cascadeCount; i++)
        {
            projectileAngles.Add(angleSpread * i);
            projectileAngles.Add(-angleSpread * i);
        }
    }

    protected override void FixedUpdate() // Fire a fan of projectiles based on cascade count and angles
    {
        base.FixedUpdate();

        if (!isVisible || isLeaving) return;

        if (currentCooldown <= 0)
        {
            currentCooldown = shotCooldown;

            var projectileList =
                projectileAngles
                    .Select(x =>
                        Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<RedProjectile>())
                    .ToList();

            var items = projectileList.Zip(projectileAngles, (x, y) => new { projectile = x, angle = y });
            foreach (var item in items) // Used Linq above for conciseness and speed
            {
                item.projectile.InitAngle(item.angle); // set the angle for the projectile
                item.projectile.InitOffset(xOffset); // offset based on ship width, so it doesn't fire from the centre
            }

            return;
        }

        currentCooldown -= Time.fixedDeltaTime;
    }
}