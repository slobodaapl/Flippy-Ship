using UnityEngine;

public class BlueProjectile : BaseEnemyProjectile
{
    public float sineMultiplier = 2;
    public float oscillationMultiplier = 2;
    private float baseY;

    private bool sineOffset;
    private float startTime;

    private void Start()
    {
        baseY = rgbd.position.y;
        startTime = Time.time;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var offset = sineOffset ? Mathf.PI : 0; // To make it wiggle opposite of the first projectile
        var y = baseY + Mathf.Sin(offset + (Time.time - startTime) * oscillationMultiplier) * sineMultiplier;
        rgbd.MovePosition(new Vector2(rgbd.position.x - defaultUnitSpeed * Time.fixedDeltaTime, y));
    }

    public void TriggerSineOffset()
    {
        sineOffset = true;
    }
}