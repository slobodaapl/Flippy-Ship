using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Impactable<WallSpawnable>
{
    //private static bool cleaned;
    
    // private void ClearInvis()
    // {
    //     cleaned = true;
    //     var parent = transform.parent;
    //
    //     if (parent != null)
    //     {
    //         foreach (Transform child in parent.transform)
    //             if (child.CompareTag("Wall"))
    //                 if (!child.GetComponent<Renderer>().isVisible)
    //                     Destroy(child.gameObject);
    //     }
    // }

    void FixedUpdate()
    {
        var pos = rgbd.position;
        //if (pos.x <= 0 && !cleaned) ClearInvis();
        rgbd.MovePosition(pos + new Vector2(-Time.fixedDeltaTime * defaultUnitSpeed * TimeTracker.GetMoveMultiplier(), 0));
    }
}
