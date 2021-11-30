using System.Linq;
using UnityEngine;

public sealed class MineSpawnable : Spawnable<MineSpawnable>
{
    private const int MaxMines = 5;
    private int CurrentMines;

    public override bool CheckConstraints()
    {
        return CurrentMines <= MaxMines;
    }

    public override void SpawnInstantiate()
    {
        var valid_patterns = patterns.Where(x => x.transform.childCount <= (MaxMines - CurrentMines)).ToList();
        var selected_pattern = SpawnChoice(valid_patterns);

        Instantiate(selected_pattern, new Vector3(xSpawnCoord, ySpawnCoord, 0), new Quaternion());
    }

    public override void UpdateConstraints(GameObject obj)
    {
        var children = obj.transform.childCount;
        CurrentMines -= children == 0 ? 1 : children;
    }
    
    public override bool IsSpawnable()
    {
        return TimeTracker.TickEnemyMine();
    }
}
