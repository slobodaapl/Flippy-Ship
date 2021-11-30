using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawnable<T> : Spawnable where T : Spawnable<T>
{
    public List<GameObject> Patterns;

    // XRange is left to right, YRange is top to bottom
    private (float, float) XRange { get; set; }
    private (float, float) YRange { get; set; }

    private static readonly Lazy<T> Lazy =
        new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

    public static T Instance => Lazy.Value;
    
    public GameObject SpawnChoice(List<GameObject> filteredPrefabs)
    {
        return filteredPrefabs.PickRandom();
    }

    public void InitValues((float, float) xrange, (float, float) yrange, List<GameObject> patterns)
    {
        Patterns = patterns;
        XRange = xrange;
        YRange = yrange;
    }
    public abstract bool CheckConstraints();
    public abstract List<GameObject> GetSpawnable(List<GameObject> prefabs);
    public abstract void UpdateConstraints(GameObject obj);
    
}

public abstract class Spawnable
{
    public float lastspawn;
    
    public abstract bool IsSpawnable();
}