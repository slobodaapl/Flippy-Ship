using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayController : MonoBehaviour
{
    public Camera myCam;
    private RaycastHit2D[] hit;

    private bool MouseOverUIElement => // Check if mouse is over visible active UI elements
        EventSystem.current.currentSelectedGameObject != null &&
        EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("UI");

    private void Start()
    {
        myCam = Camera.main;
    }

    // To shorthand the explanation.. Check if we clicked on anything in the scene
    // Then filter out the player ship
    // Then we check if we clicked on Debris
    // If yes, we damage the debris and don't change the player's direction
    // If we clicked on anything else that's not UI, we change the player's direction
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var point = myCam.ScreenPointToRay(Input.mousePosition);
            var gotHit = false;
            hit = Physics2D.RaycastAll(point.origin, point.direction);
            var hitList = hit.Where(x => x.transform.name != "Ship").ToList();
            if (hitList.Count != 0)
                foreach (var obj in hitList)
                {
                    var debris = obj.transform.gameObject.GetComponent<PhysicalDebris>();
                    if (debris != null)
                    {
                        debris.ClickDamage();
                        gotHit = true;
                    }
                }

            if (!gotHit && !MouseOverUIElement)
                PlayerShip.SwitchDirection();
        }
    }
}