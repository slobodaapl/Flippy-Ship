using UnityEngine;

public class Impactable : MonoBehaviour
{
    public int collisionDamage = 1;
    public float defaultUnitSpeed = 1;
    
    protected Rigidbody2D rgbd;
    
    public int GetDamage()
    {
        return collisionDamage;
    }

    public void DestroyOnCollission()
    {
        Destroy(gameObject);
    }
    
}

public class Impactable<T> : Impactable where T : Spawnable<T>
{
    protected T spawnable;
    
    protected void Start()
    {
        spawnable = Spawnable<T>.Instance;
        rgbd = GetComponent<Rigidbody2D>();
    }
    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        spawnable.UpdateConstraints(gameObject);
    }
}