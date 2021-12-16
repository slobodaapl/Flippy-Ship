using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjectile : BaseEnemyProjectile
{
    public void InitAngle(float angle)
    {
        angle *= Mathf.Deg2Rad;
        rgbd.velocity = defaultUnitSpeed * new Vector2( -Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
