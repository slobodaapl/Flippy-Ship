using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PhysicalDebris : Impactable<DebrisSpawnable>
{
    private bool falling;

    void FixedUpdate()
    {
        if (!falling)
            rgbd.velocity = new Vector2(-TimeTracker.GetMoveMultiplier() * defaultUnitSpeed, 0);

        if (transform.position.x + rgbd.velocity.x <= -12)
        {
            rgbd.isKinematic = false;
            falling = true;
        }
    }
}
