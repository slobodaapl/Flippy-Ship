using UnityEngine;

public class WallSpawnable : Spawnable<WallSpawnable> // Check Spawnable
{
    public override bool IsSpawnable()
    {
        return TimeTracker.TickPillar();
    }

    public override void SpawnInstantiate()
    {
        var selected_pattern = SpawnChoice(patterns);
        Instantiate(selected_pattern, new Vector3(xSpawnCoord, ySpawnCoord, 0), new Quaternion());
    }

    public override bool CheckConstraints()
    {
        // No constraints to update, it's only time-dependent
        return true;
    }

    public override void UpdateConstraints(GameObject obj)
    {
        // No constraints needed
    }
}