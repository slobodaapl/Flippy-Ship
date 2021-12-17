using UnityEngine;

public abstract class PlayerObserver : MonoBehaviour // Generic PlayerObserver. I only used HealthChanged for now
{
    protected PlayerShip player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
    }

    public abstract void HealthChanged(bool damage);
}