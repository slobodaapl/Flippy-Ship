using UnityEngine;

public class PhysicalDebris : Impactable<DebrisSpawnable>
{
    private static AddDestruction addDestructionPoints;
    public int health = 3;
    public int destructionPointWorth = 1;
    
    public GameObject ropePrefab;
    public GameObject clickPrefab;

    private bool falling;

    private GameObject clickObj;

    private void Start()
    {
        var newPos = transform.position;
        var ropePos = newPos + new Vector3(-0.025f, 2, 0);
        
        var obj = Instantiate(ropePrefab, ropePos, new Quaternion()); // Cool rope for visual happiness
        var objComp = obj.GetComponent<PlainMove>();
        objComp.SetDefaultUnitSpeed(defaultUnitSpeed);

        if (TimeTracker.tutorial) // Create little tutorial image for the first time a debris is visible
        {
            TimeTracker.tutorial = false;
            var clickPos = newPos + new Vector3(1.5f, -2.5f, 0);
            clickObj = Instantiate(clickPrefab, clickPos, new Quaternion());
            var clickobjComp = clickObj.GetComponent<PlainMove>();
            clickobjComp.SetDefaultUnitSpeed(defaultUnitSpeed);
        }

        if (addDestructionPoints == null)
        {
            var pointController = GameObject.FindWithTag("GameController").GetComponent<PointController>();
            addDestructionPoints = pointController.AddDestruction;
        }
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            if(clickObj != null) // Tutorial image
                Destroy(clickObj);
            
            addDestructionPoints(destructionPointWorth);
            Destroy(gameObject);
            return;
        }

        if (!falling)
            rgbd.velocity = new Vector2(-TimeTracker.GetMoveMultiplier() * defaultUnitSpeed, 0);

        if (transform.position.x + rgbd.velocity.x <= -12) 
        {
            rgbd.isKinematic = false; // I could do velocity calculation based on acceleration here.. but I'm a programmer and I'm lazy
            falling = true;
        }
    }

    public void ClickDamage()
    {
        health -= 1;
    }

    private delegate void AddDestruction(int pts);
}