using UnityEngine;

public class DarkMine : SpawnableImpactable<MineSpawnable>
{
    void FixedUpdate()
    {
        rgbd.MovePosition(rgbd.position + new Vector2(-Time.fixedDeltaTime * defaultUnitSpeed * TimeTracker.GetMoveMultiplier(), 0));
    }
}