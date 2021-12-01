using System.Linq;
using UnityEngine;

public sealed class MineSpawnable : Spawnable<MineSpawnable>
{
    private const int maxMines = 5;
    private int currentMines;

    public override bool CheckConstraints()
    {
        return currentMines <= maxMines;
    }

    public override void SpawnInstantiate()
    {
        var valid_patterns = patterns.Where(x =>
        {
            var children = x.transform.childCount;
            return (children == 0 ? 1 : children) <= (maxMines - currentMines);
        }).ToList();
        
        if (valid_patterns.Count == 0) return;
        
        var selected_pattern = SpawnChoice(valid_patterns);
        var children = selected_pattern.transform.childCount;
        currentMines += children == 0 ? 1 : children;

        Instantiate(selected_pattern, new Vector3(xSpawnCoord, ySpawnCoord, 0), new Quaternion());
    }

    public override void UpdateConstraints(GameObject obj)
    {
        var children = obj.transform.childCount;
        currentMines -= children == 0 ? 1 : children;
    }
    
    public override bool IsSpawnable()
    {
        return TimeTracker.TickEnemyMine();
    }
}
