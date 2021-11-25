using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impactable : MonoBehaviour
{
    public int collisionDamage = 1;

    private void OnBecameInvisible()
    {
        Destroy(this);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        
    }
}
