using UnityEngine;

public class Impactable : MonoBehaviour
{
    public int collisionDamage = 1;
    public float defaultUnitSpeed = 1;
    public bool isDestroyedOnImpact = true;
    
    protected Rigidbody2D rgbd;
    
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
    
    protected void Awake()
    {
        spawnable = Spawnable<T>.Instance;
        rgbd = GetComponent<Rigidbody2D>();
    }
    protected void OnBecameInvisible()
    {
        Shootable comp = GetComponent<Shootable>();
        if (comp != null)
            GameObject.FindGameObjectWithTag("Shooter").GetComponent<PlayerShooter>().DestroyCallback(gameObject);
        
        Destroy(gameObject);
        spawnable.UpdateConstraints(gameObject);
    }
}