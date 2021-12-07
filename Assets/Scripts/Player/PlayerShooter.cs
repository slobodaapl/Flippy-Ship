using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public int damage = 1;
    public int maxTargets = 1;
    public float cooldown = 2.5f;
    public float maxAngle = 15;

    public Action<Collision2D> OnCollissionEnter2D_Action;
    public Action<Collision2D> OnCollissionExit2D_Action;

    private float cooldownRemaining;
    private Dictionary<int, Shootable> shootableDict = new Dictionary<int, Shootable>();
    private GameObject playerShip;

    void Start()
    {
        playerShip = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var obj = other.gameObject;
        var component = obj.GetComponent<Shootable>();
        
        if (component != null)
            shootableDict.Add(obj.GetInstanceID(), component);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var obj = other.gameObject;
        var component = obj.GetComponent<Shootable>();
        if (component == null)
        {
            shootableDict.Remove(obj.GetInstanceID());
        }
    }

    public void DestroyCallback(GameObject obj)
    {
        shootableDict.Remove(obj.GetInstanceID());
    }

    private void Shoot(Shootable target)
    {
        target.GetShot(damage);
        cooldownRemaining = cooldown;
    }

    private List<Shootable> TestDistance(List<Shootable> candidates)
    {
        return candidates.OrderBy(
                x => Vector2.Distance(playerShip.transform.position, x.transform.position)
            ).Take(maxTargets).ToList();
    }
    
    private void FixedUpdate()
    {
        List<Shootable> toBeShot = new List<Shootable>();
        if (cooldownRemaining <= 0)
        {
            foreach (var kvp in shootableDict)
            {
                var targetVec = kvp.Value.transform.position - playerShip.transform.position;
                var forwardVec = playerShip.transform.right;
                var angle = Vector3.SignedAngle(targetVec, forwardVec, Vector3.up);

                if (Mathf.Abs(angle) <= maxAngle) toBeShot.Add(kvp.Value);
            }

            foreach (var s in TestDistance(toBeShot)) Shoot(s);
        }
        else
        {
            cooldownRemaining -= Time.fixedDeltaTime;
        }
    }
}
