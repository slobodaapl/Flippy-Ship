using UnityEngine;

public class Wall : Impactable<WallSpawnable>
{
    private void FixedUpdate() // Just moves right to left
    {
        var pos = rgbd.position;
        rgbd.MovePosition(pos + new Vector2(-Time.fixedDeltaTime * defaultUnitSpeed * TimeTracker.GetMoveMultiplier(),
            0));
    }
}