using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Impactable<T> : MonoBehaviour where T : Spawnable<T>
{
    public int collisionDamage = 1;
    public float defaultUnitSpeed = 1;

    protected T spawnable;
    protected Rigidbody2D rgbd;

    protected void Start()
    {
        spawnable = Spawnable<T>.Instance;
        rgbd = GetComponent<Rigidbody2D>();
    }
    protected void OnBecameInvisible()
    {
        spawnable.UpdateConstraints(gameObject);
        Destroy(gameObject);
    }
}
