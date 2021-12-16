using UnityEngine;

public class BlueEnemyShip : BaseShip
{

    override protected void FixedUpdate()
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