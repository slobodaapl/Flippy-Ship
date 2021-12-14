using UnityEngine;

public abstract class PlayerObserver : MonoBehaviour
{
    protected PlayerShip player;
    
    public abstract void HealthChanged(bool damage);

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
    }
}