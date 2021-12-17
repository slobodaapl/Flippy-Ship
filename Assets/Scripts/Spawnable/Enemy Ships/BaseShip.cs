using UnityEngine;

public class BaseShip : Impactable<EnemySpawnable> // Base enemy ship
{
    public EnemyShipType type; // Type of the ship (RED or BLUE.. I removed GREEN cause its concept didn't work gameplay-wise)
    public GameObject projectile; // The projectile to use 
    public float shotCooldown = 0.75f; //Time between shots
    protected float currentCooldown; // Cooldown between shots left
    protected bool isLeaving; // Whether it's now leaving the screen

    protected bool isVisible;

    private PlayerShip ship;
    protected float xOffset;

    protected virtual void Start()
    {
        ship = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
        xOffset = -GetComponent<SpriteRenderer>().bounds.size.x / 2; // Offset for the projectiles, to shoot from nose
    }

    protected virtual void FixedUpdate()
    {
        var pos = rgbd.position;

        if (!isVisible) // If it's not visible, move into the screen until it is, towards the middle
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
            rgbd.MovePosition(pos + Time.fixedDeltaTime * defaultUnitSpeed *
                new Vector2(-1, Mathf.Sign(rgbd.position.y) * 10));
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }
}