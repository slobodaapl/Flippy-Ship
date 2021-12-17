using UnityEngine;

public class DarkMine : Impactable<MineSpawnable>
{
    private void FixedUpdate() // Just moves slowly to the left in groups
    {
        rgbd.MovePosition(rgbd.position +
                          new Vector2(-Time.fixedDeltaTime * defaultUnitSpeed * TimeTracker.GetMoveMultiplier(), 0));
    }
}