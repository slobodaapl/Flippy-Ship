using System.Collections.Generic;
using UnityEngine;

public class WhiteMine : Impactable<MineSpawnable>
{
    public float oscillationSpeedMultiplier = 2;
    public float oscillationRangeMultiplier = 2;
    public bool offsetable = true;
    
    private float initialY;
    private float offset;
    private float fixedOffset;

    public void setFixedOffset(float newOffset)
    {
        fixedOffset = newOffset;
    }

    protected virtual void Start()
    {
        initialY = transform.position.y;
        offset = Random.Range(90, 270);
    }
    
    protected void FixedUpdate()
    {
        var y = Mathf.Sin(Time.time * oscillationSpeedMultiplier + (offsetable ? offset : fixedOffset)) * oscillationRangeMultiplier;
        rgbd.MovePosition(new Vector2(rgbd.position.x - Time.fixedDeltaTime * defaultUnitSpeed, initialY+y));
    }
}