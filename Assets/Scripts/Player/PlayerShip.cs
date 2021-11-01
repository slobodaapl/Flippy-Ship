using System;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public const float startDelay = 1;
    public float turnRateDegreesSecond = 60;
    public float maxTurnAngle = 90;
    public float maxAngleSineSpeed = 1;
    
    private bool start = false;
    private bool turningUp = false;
    private float currentRotation = 0;

    private void AdjustAngle()
    {
        if(!start)
            return;
        
        var turnDir = turningUp ? 1 : -1;
        var turnAngle = turnRateDegreesSecond * Time.fixedDeltaTime * turnDir;

        var finalAngle = transform.eulerAngles + Vector3.forward * turnAngle;

        if (Math.Abs(currentRotation + turnAngle) >= maxTurnAngle)
        {
            currentRotation = turnDir * maxTurnAngle;
            finalAngle = Vector3.forward * currentRotation;
        }
        else
        {
            currentRotation += turnAngle;
        }
        
        transform.eulerAngles = finalAngle;
    }

    private void AdjustPos()
    {
        var currentPos = transform.position;
        currentPos.y += maxAngleSineSpeed * (currentRotation / maxTurnAngle);
        transform.position = currentPos;
    }
    private void FixedUpdate()
    {
        AdjustAngle();
        AdjustPos();
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

    private void OnBecameInvisible()
    {
        Destroy(this);
        Application.Quit();
    }
}
