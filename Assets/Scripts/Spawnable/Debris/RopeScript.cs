using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    private float defaultUnitSpeed;
    private Rigidbody2D rgbd;

    public void SetDefaultUnitSpeed(float speed)
    {
        defaultUnitSpeed = speed;
    }

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.x <= -17)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rgbd.velocity = new Vector2(-TimeTracker.GetMoveMultiplier() * defaultUnitSpeed, 0);
    }
}
