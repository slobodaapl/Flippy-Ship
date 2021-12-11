using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Spawnable<T> : Spawnable where T : Spawnable<T>
{
    private static Lazy<T> Lazy;

    public static T Instance => Lazy.Value;

    void Awake()
    {
        Lazy = new Lazy<T>(() => GetComponent<T>());
    }
    
    // The below is the original code.. It would create a 'new Monobehavior' which is illegal. The above works
    // but needs testing, unsure if it works correctly.
    
    // private static readonly Lazy<T> Lazy =
    //     new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);
    //
    // public static T Instance => Lazy.Value;
    
    protected GameObject SpawnChoice(List<GameObject> filteredPrefabs)
    {
        return filteredPrefabs.PickRandom();
    }

}

public abstract class Spawnable : MonoBehaviour
{
    public GameObject topLeftSpawnMarker;
    public GameObject bottomRightSpawnMarker;
    public List<GameObject> patterns;

    public bool horizontalSpawn;
    
    // XRange is left to right, YRange is top to bottom
    protected (float, float) xRange;
    protected (float, float) yRange;
    protected bool initialized;

    protected float xSpawnCoord;
    protected float ySpawnCoord;

    public PlayerShooter playerShooter;

    public abstract bool IsSpawnable();
    public abstract void SpawnInstantiate();
    public abstract bool CheckConstraints();
    public abstract void UpdateConstraints([CanBeNull] GameObject obj);

    void Start()
    {
        playerShooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<PlayerShooter>();
    }
    
    void OnDestroy()
    {
        try
        {
            if (GameController.isUnloading)
                return;

            UpdateConstraints(gameObject);
        }
        catch (NullReferenceException)
        {
            // Nothing. This is just a unfortunate patch due to problems with Lazy initialization
        }
    }

    public virtual bool Spawn()
    {
        if (!IsSpawnable()) return false;
        if (!initialized) InitValues();

        xSpawnCoord = CalcUtil.GetCoord(xRange.Item1, xRange.Item2, false);
        ySpawnCoord = CalcUtil.GetCoord(yRange.Item1, yRange.Item2, horizontalSpawn);

        SpawnInstantiate();
        return true;
    }
    
    protected void InitValues()
    {
        var postopleft = topLeftSpawnMarker.transform.position;
        var posbottomright = bottomRightSpawnMarker.transform.position;

        xRange = (postopleft.x, posbottomright.x);
        yRange = (posbottomright.y, postopleft.y);
        initialized = true;
    }
    
}