using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjectile : MonoBehaviour
{

    public float baseDotVelocity;
    
    private Rigidbody2D rgbd;

    public void InitVelocity(float angle) // Angle from Vector3.left
    {
        rgbd.velocity = new Vector2(baseDotVelocity * Mathf.Cos(angle), baseDotVelocity * Mathf.Sin(angle));
    }
    
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
