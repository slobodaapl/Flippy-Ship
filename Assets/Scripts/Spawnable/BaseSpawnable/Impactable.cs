using UnityEngine;

public class Impactable : MonoBehaviour // Whether object can be impacted by player. It can move and get destroyed if we want when it's collided with
{
    public int collisionDamage = 1;
    public float defaultUnitSpeed = 1;
    public bool isDestroyedOnImpact = true;

    protected Rigidbody2D rgbd;

    protected virtual void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    public int GetDamage()
    {
        return collisionDamage;
    }

    public void DestroyOnCollission()
    {
        if (isDestroyedOnImpact)
            Destroy(gameObject);
    }
}

public class Impactable<T> : Impactable where T : Spawnable<T> // A special Impactable that is tracked by Spawners, to update their constraints on spawning
{
    protected T spawnable; // The inheritable spawner singleton

    protected override void Awake()
    {
        base.Awake();
        spawnable = Spawnable<T>.Instance;
    }

    protected void OnBecameInvisible()
    {
        var comp = GetComponent<Shootable>(); // If shootable, remove it from tracked shootabled by PlayerShooter
        if (comp != null)
            spawnable.playerShooter.DestroyCallback(gameObject);

        Destroy(gameObject);
        spawnable.UpdateConstraints(gameObject); // Update spawner
    }
}