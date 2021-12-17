using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Spawnable<T> : Spawnable where T : Spawnable<T> // Special inheritable singleton to create unique spawners
{
    private static Lazy<T> Lazy; // Using Lazy initialization to work well with unity's threads, plus generic to make it inheritable

    public static T Instance => Lazy.Value;

    private void Awake()
    {
        // It's a bit complicated to explain how this can work
        // It's like jumping from one speeding train to another, and keeping them connected with a rope
        // But like.. in unity with threads
        
        Lazy = new Lazy<T>(() => GetComponent<T>());
    }

    // The below is the original code.. It would create a 'new Monobehavior' which is illegal. The above works well.

    // private static readonly Lazy<T> Lazy =
    //     new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);
    //
    // public static T Instance => Lazy.Value;

    protected GameObject SpawnChoice(List<GameObject> filteredPrefabs) // Chooses a random prefab from list of prefabs
    {
        return filteredPrefabs.PickRandom(); // PickRandom is an IEnumerable extension. See EnumerableExtension.cs
    }
}

// A base spawnable that the singleton inherits from
// It's done this way to be able to use a non-generic form of the class for abstraction in other classes (like for lists)
public abstract class Spawnable : MonoBehaviour
{
    // I'm using spawn markers instead of coordinates cause it's visual and easy to work with, and easy to adjust visually
    public GameObject topLeftSpawnMarker;
    public GameObject bottomRightSpawnMarker;
    public List<GameObject> patterns;

    public bool horizontalSpawn; // Whether to use a horizontal range between markers on x axis, or if false, use range of y axis for spawning

    public PlayerShooter playerShooter;
    protected bool initialized;

    // XRange is left to right, YRange is top to bottom
    protected (float, float) xRange;
    protected (float, float) yRange;

    protected float xSpawnCoord;
    protected float ySpawnCoord;

    private void Start()
    {
        playerShooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<PlayerShooter>();
    }

    private void OnDestroy()
    {
        try
        {
            if (GameController.isUnloading)
                return;

            UpdateConstraints(gameObject); // Update spawner that the spawned object got deleted, so we can spawn more (depending on constraints defined individually in each Spawnable)
        }
        catch (NullReferenceException)
        {
            // Nothing. This is just a unfortunate patch due to problems with Lazy initialization
        }
    }

    public abstract bool IsSpawnable(); // This checks if the spawn cooldown is done so we can spawn more (time-based)
    public abstract void SpawnInstantiate(); // Custom spawning of the objects. We may need additional initialization, so we let Spawnables handle it
    public abstract bool CheckConstraints(); // Custom constraints to check if we can spawn another one. Like number of mines in scene, etc.
    public abstract void UpdateConstraints([CanBeNull] GameObject obj); // Update the constraints, like reducing currently spawned mines if one gets destroyed so we can spawn more

    public virtual bool Spawn()
    {
        if (!IsSpawnable()) return false;
        if (!initialized) InitValues();

        // Random spawn position based on markers
        xSpawnCoord = CalcUtil.GetCoord(xRange.Item1, xRange.Item2, false);
        ySpawnCoord = CalcUtil.GetCoord(yRange.Item1, yRange.Item2, horizontalSpawn);

        SpawnInstantiate();
        return true;
    }

    protected void InitValues() // Init the spawning range
    {
        var postopleft = topLeftSpawnMarker.transform.position;
        var posbottomright = bottomRightSpawnMarker.transform.position;

        xRange = (postopleft.x, posbottomright.x);
        yRange = (posbottomright.y, postopleft.y);
        initialized = true;
    }
}