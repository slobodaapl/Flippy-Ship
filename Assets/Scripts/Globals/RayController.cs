using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayController : MonoBehaviour
{
    public Camera myCam;
    private RaycastHit2D[] hit;
    
    private bool MouseOverUIElement =>
        EventSystem.current.currentSelectedGameObject != null &&
        EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("UI");

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray point = myCam.ScreenPointToRay(Input.mousePosition);
            bool gotHit = false;
            hit = Physics2D.RaycastAll(point.origin, point.direction);
            var hitList = hit.Where(x => x.transform.name != "Ship").ToList();
            if(hitList.Count != 0)
            {
                foreach (var obj in hitList)
                {
                    var debris = obj.transform.gameObject.GetComponent<PhysicalDebris>();
                    if (debris != null)
                    {
                        debris.ClickDamage();
                        gotHit = true;
                    }
                }
            }

            if (!gotHit && !MouseOverUIElement)
                PlayerShip.SwitchDirection();
        }
    }
}
