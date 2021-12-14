using System;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public float health = 1;
    public int destructionPointWorth = 1;

    private delegate void AddDestruction(int pts);
    private static AddDestruction addDestructionPoints;

    void Start()
    {
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
        if (health <= 0)
        {
            addDestructionPoints(destructionPointWorth);
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Shooter").GetComponent<PlayerShooter>().DestroyCallback(gameObject);
        }
    }
}