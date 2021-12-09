using UnityEngine;

public class Impactable : MonoBehaviour
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

public class Impactable<T> : Impactable where T : Spawnable<T>
{
    protected T spawnable;
    
    protected override void Awake()
    {
        base.Awake();
        spawnable = Spawnable<T>.Instance;
    }
    protected void OnBecameInvisible()
    {
        Shootable comp = GetComponent<Shootable>();
        if (comp != null)
            spawnable.playerShooter.DestroyCallback(gameObject);
        
        Destroy(gameObject);
        spawnable.UpdateConstraints(gameObject);
    }
}