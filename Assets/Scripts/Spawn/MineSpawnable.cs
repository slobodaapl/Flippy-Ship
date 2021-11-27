using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class MineSpawnable : Spawnable<MineSpawnable>
{
    public const int MaxMines = 5;
    public int CurrentMines = 0;

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
        CurrentMines -= obj.transform.childCount;
    }
}
