using System.Collections.Generic;
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

    public override List<GameObject> GetSpawnable(List<GameObject> prefabs)
    {
        return prefabs.Where(x => x.transform.childCount <= (MaxMines - CurrentMines)).ToList();
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
