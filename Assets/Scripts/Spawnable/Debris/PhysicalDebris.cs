using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PhysicalDebris : Impactable<DebrisSpawnable>
{
    public int health = 3;
    public int destructionPointWorth = 1;
    public GameObject ropePrefab;

    private bool falling;
    
    private delegate void AddDestruction(int pts);
    private static AddDestruction addDestructionPoints;

    void Start()
    {
        var newPos = transform.position;
        newPos += new Vector3(-0.025f, 2, 0);
        var obj = Instantiate(ropePrefab, newPos, new Quaternion());
        var objComp = obj.GetComponent<RopeScript>();
        objComp.SetDefaultUnitSpeed(defaultUnitSpeed);
        
        if (addDestructionPoints == null)
        {
            var pointController = GameObject.FindWithTag("GameController").GetComponent<PointController>();
            addDestructionPoints = pointController.AddDestruction;
        }
    }

    public void ClickDamage()
    {
        health -= 1;
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            addDestructionPoints(destructionPointWorth);
            Destroy(gameObject);
            return;
        }

        if (!falling)
            rgbd.velocity = new Vector2(-TimeTracker.GetMoveMultiplier() * defaultUnitSpeed, 0);

        if (transform.position.x + rgbd.velocity.x <= -12)
        {
            rgbd.isKinematic = false;
            falling = true;
        }
    }
}
