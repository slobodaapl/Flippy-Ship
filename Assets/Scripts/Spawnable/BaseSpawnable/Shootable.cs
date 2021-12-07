using UnityEngine;

public class Shootable : MonoBehaviour
{
    public int health = 1;

    public void GetShot(int damage)
    {
        health -= damage;
        CheckAlive();
    }

    private void CheckAlive()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Shooter").GetComponent<PlayerShooter>().DestroyCallback(gameObject);
        }
    }
}