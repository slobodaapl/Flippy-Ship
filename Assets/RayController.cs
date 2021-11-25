using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class RayController : MonoBehaviour
{
    public Camera myCam;
    private RaycastHit2D hit;

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray point = myCam.ScreenPointToRay(Input.mousePosition);
            
            hit = Physics2D.Raycast(point.origin, point.direction);

            if(hit.collider != null)
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
}
