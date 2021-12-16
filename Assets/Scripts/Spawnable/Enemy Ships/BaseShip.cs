using System;
using UnityEngine;

public class BaseShip : Impactable<EnemySpawnable>
{
    public EnemyShipType type;
    public GameObject projectile;
    public float shotCooldown = 0.75f; //Time between shots
    
    protected bool isVisible;
    protected bool isLeaving;
    protected float xOffset;
    protected float currentCooldown;

    private PlayerShip ship;
    
    protected virtual void Start()
    {
        ship = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        xOffset = -GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }
    
    protected virtual void FixedUpdate()
    {
        var pos = rgbd.position;

        if (!isVisible)
        {
            rgbd.MovePosition(pos - new Vector2(0, Mathf.Sign(pos.y) * Time.fixedDeltaTime * defaultUnitSpeed));
            return;
        }

        if (!isLeaving)
        {
            var shipPos = ship.Get2DPos();
            var yDelta = shipPos.y - pos.y;


            // Doesn't speed up with the rest of the objects over time, so no Timetracket.GetSpeedMultiplier()
            // Follows the player's y position
            rgbd.MovePosition(pos + Time.fixedDeltaTime * defaultUnitSpeed * new Vector2(-1, yDelta));

            if (rgbd.position.x <= 0)
                isLeaving = true;
        }
        
        // After it gets to mid-screen, leave. Hastily.
        if (isLeaving)
        {
            rgbd.MovePosition(pos + Time.fixedDeltaTime * defaultUnitSpeed *
                new Vector2(-1, Mathf.Sign(rgbd.position.y) * 10));
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }
}
