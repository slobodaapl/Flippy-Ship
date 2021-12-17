using UnityEngine;

public class WhiteMine : Impactable<MineSpawnable>
{
    public float oscillationSpeedMultiplier = 2;
    public float oscillationRangeMultiplier = 2;
    public bool offsetable = true;
    private float fixedOffset;

    private float initialY;
    private float offset;

    protected virtual void Start()
    {
        initialY = transform.position.y;
        offset = Random.Range(90, 270);
    }

    protected void FixedUpdate() // Wiggles in a cross group or other group, making it a bit harder to dodge, plus moves faster than dark
    {
        var y = Mathf.Sin(Time.timeSinceLevelLoad * oscillationSpeedMultiplier + (offsetable ? offset : fixedOffset)) *
                oscillationRangeMultiplier;
        rgbd.MovePosition(new Vector2(rgbd.position.x - Time.fixedDeltaTime * defaultUnitSpeed, initialY + y));
    }

    public void setFixedOffset(float newOffset)
    {
        fixedOffset = newOffset;
    }
}