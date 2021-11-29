using UnityEngine;

public class DarkMine : Impactable<MineSpawnable>
{
    void FixedUpdate()
    {
        rgbd.MovePosition(rgbd.position + new Vector2(-Time.fixedDeltaTime * defaultUnitSpeed * TimeTracker.GetMoveMultiplier(), 0));
    }
}