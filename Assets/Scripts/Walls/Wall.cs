using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Impactable<WallSpawnable>
{
    private void OnBecameVisible()
    {
        var parent = transform.parent;

        if (parent != null)
        {
            foreach (Transform child in parent.transform)
                if (child.CompareTag("Wall"))
                    if (!child.GetComponent<Renderer>().isVisible)
                        Destroy(child);
        }
    }

    void FixedUpdate()
    {
        rgbd.MovePosition(rgbd.position + new Vector2(-Time.fixedDeltaTime * defaultUnitSpeed * TimeTracker.GetMoveMultiplier(), 0));
    }
}
