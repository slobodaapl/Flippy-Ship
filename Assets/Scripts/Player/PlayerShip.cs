using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public const float startDelay = 1;
    public float turnRateDegreesSecond = 60;
    
    private bool start = false;
    private bool turningUp = false;
    private float currentRotation = 0;

    private void FixedUpdate()
    {
        if(!start)
            return;
        
        var turnDir = turningUp ? 1 : -1;
        var turnAngle = turnRateDegreesSecond * Time.fixedDeltaTime * turnDir;

        var finalAngle = transform.eulerAngles + Vector3.forward * turnAngle;

        if (Math.Abs(currentRotation + turnAngle) >= 90)
        {
            currentRotation = turnDir * 90;
            finalAngle = Vector3.forward * currentRotation;
        }
        else
        {
            currentRotation += turnAngle;
        }
        transform.eulerAngles = finalAngle;
    }

    void Update()
    {
        if (Time.time <= startDelay)
            return;
        
        if (!start)
            start = true;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            turningUp = !turningUp;

    }
}
