using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impactable<T> : MonoBehaviour where T : Spawnable<T>
{
    public int collisionDamage = 1;
    private T spawnable;

    void Start()
    {
        spawnable = Spawnable<T>.Instance;
    }
    private void OnBecameInvisible()
    {
        spawnable.UpdateConstraints(gameObject);
        Destroy(gameObject);
    }
}
