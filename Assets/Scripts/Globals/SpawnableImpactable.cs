using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SpawnableImpactable<T> : Impactable where T : Spawnable<T>
{
    protected T spawnable;
    
    protected void Start()
    {
        spawnable = Spawnable<T>.Instance;
        rgbd = GetComponent<Rigidbody2D>();
    }
    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        spawnable.UpdateConstraints(gameObject);
    }
}
