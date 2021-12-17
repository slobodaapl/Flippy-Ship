using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public float damage = 1; // Damage the player's shots deal
    public int maxTargets = 1; // How many targets can we shoot at, at once
    public float cooldown = 2.5f; // Cooldown between shots. I didn't include the cooldown upgrade, was too OP
    public float maxAngle = 5; // What angle the target be max from the nose of the ship to be hit

    public GameObject playerProjectile;
    private CircleCollider2D circleCollider;

    private float cooldownRemaining;
    private GameObject playerShip;
    private readonly Dictionary<int, Shootable> shootableDict = new Dictionary<int, Shootable>();

    private void Start()
    {
        playerShip = transform.parent.gameObject;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // We basically remember a list of objects that are within the radius of our ship to be shot
    // We add an object when it comes in range if it has the Shootable component
    // Every update we check if it's under the right angle, and if yes, add it to toBeShot as a shootable candidate
    // We then order the toBeShot candidates by euclidean distance from the ship
    // Then we shoot the closest x enemies, based on maxTargets
    private void FixedUpdate()
    {
        var toBeShot = new List<Shootable>();
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

    private void OnTriggerEnter2D(Collider2D other) // Add object to be tracked for shooting
    {
        var obj = other.gameObject;
        var component = obj.GetComponent<Shootable>();

        if (component != null)
        {
            if(!shootableDict.ContainsKey(obj.GetInstanceID()))
                shootableDict.Add(obj.GetInstanceID(), component);
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Removed tracked object when it leaves range
    {
        var obj = other.gameObject;
        var component = obj.GetComponent<Shootable>();
        if (component == null) shootableDict.Remove(obj.GetInstanceID());
    }

    public void OffsetColliderRadius(float offset) // Used by upgrade to increase range
    {
        circleCollider.radius += offset;
    }

    public float GetColliderRadius() // Used by upgrade to check eligibility of the upgrade
    {
        return circleCollider.radius;
    }

    public void DestroyCallback(GameObject obj) // If tracked object is destroyed before OnTriggerExit is called, to stop tracking it
    {
        shootableDict.Remove(obj.GetInstanceID());
    }

    private void GenerateProjectile(Shootable target) // Create the weird wiggly lazer that shoots stuff. Idk why it doesn't track the object properly.
    {
        var targetVec = target.transform.position - playerShip.transform.position;
        var forwardVec = playerShip.transform.right;
        var angle = Vector3.SignedAngle(targetVec, forwardVec, Vector3.up);
        var scale = Vector3.Distance(gameObject.transform.position, target.gameObject.transform.position);
        var shotObj = Instantiate(playerProjectile, playerShip.transform.position, Quaternion.Euler(0, 0, angle))
            .GetComponent<PlayerProjectile>();

        shotObj.transform.localScale = new Vector3(scale, 1, 1);
    }

    private void Shoot(Shootable target) // Generate the wiggly lazer thing from player to enemy, and deal damage, then put shooting on cooldown
    {
        GenerateProjectile(target);
        target.GetShot(damage);
        cooldownRemaining = cooldown;
    }

    private List<Shootable> TestDistance(List<Shootable> candidates) // Order shootables by distance and take maxTarget candidates
    {
        return candidates.OrderBy(
            x => Vector2.Distance(playerShip.transform.position, x.transform.position)
        ).Take(maxTargets).ToList();
    }
}