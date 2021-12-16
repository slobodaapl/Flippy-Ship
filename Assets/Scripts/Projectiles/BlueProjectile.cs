using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjectile : BaseEnemyProjectile
{
    public float sineMultiplier = 2;
    public float oscillationMultiplier = 2;
    
    private bool sineOffset;
    private float baseY;
    private float startTime;

    public void TriggerSineOffset()
    {
        sineOffset = true;
    }
    
    override protected void FixedUpdate()
    {
        base.FixedUpdate();

        var offset = sineOffset ? Mathf.PI : 0;
        var y = baseY + Mathf.Sin(offset + (Time.time - startTime) * oscillationMultiplier) * sineMultiplier;
        rgbd.MovePosition(new Vector2(rgbd.position.x - defaultUnitSpeed * Time.fixedDeltaTime, y));
    }

    void Start()
    {
        baseY = rgbd.position.y;
        startTime = Time.time;
    }
}
