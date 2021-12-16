using System;
using UnityEngine;

public abstract class BaseEnemyProjectile : Impactable
{
    
    public void InitOffset(float offset)
    {
        rgbd.MovePosition(rgbd.position + new Vector2(offset, 0));
    }

    protected void FixedUpdate()
    {
        if (rgbd.position.x <= -20)
            Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}