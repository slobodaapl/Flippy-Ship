using UnityEngine;

public class Shootable : MonoBehaviour // Candidates to be shot by the ship
{
    private static AddDestruction addDestructionPoints; // delegate from PointController to save its method for use
    public float health = 1;
    public int destructionPointWorth = 1;
    private PlayerShooter shooter;

    private void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<PlayerShooter>();

        if (addDestructionPoints == null)
        {
            var pointController = GameObject.FindWithTag("GameController").GetComponent<PointController>();
            addDestructionPoints = pointController.AddDestruction;
        }
    }

    public void GetShot(float damage)
    {
        health -= damage;
        CheckAlive();
    }

    private void CheckAlive()
    {
        if (health > 0) return;

        addDestructionPoints(destructionPointWorth); // Add points when destroyed
        Destroy(gameObject);
        shooter.DestroyCallback(gameObject); // If it's destroyed by shooting, remove from tracked shootable objects
        // Also a fallback of this in Impactable<Spawnable> if it's destroyed by other means
    }

    private delegate void AddDestruction(int pts);
}