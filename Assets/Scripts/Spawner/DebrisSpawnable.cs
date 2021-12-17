using UnityEngine;

public class DebrisSpawnable : Spawnable<DebrisSpawnable> // Check Spawnable
{
    private bool isDebrisSpawned;

    public override bool IsSpawnable()
    {
        return TimeTracker.TickDebris();
    }

    public override void SpawnInstantiate()
    {
        var selected_pattern = SpawnChoice(patterns);
        isDebrisSpawned = true;
        Instantiate(selected_pattern, new Vector3(xSpawnCoord, ySpawnCoord, 0), new Quaternion());
    }

    public override bool CheckConstraints()
    {
        return !isDebrisSpawned;
    }

    public override void UpdateConstraints(GameObject obj)
    {
        isDebrisSpawned = false;
    }
}