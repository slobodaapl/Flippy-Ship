using UnityEngine;

public abstract class BaseEnemyProjectile : Impactable
{

    public void InitOffset(float offset)
    {
        rgbd.MovePosition(rgbd.position + new Vector2(offset, 0));
    }

    void CustomDestroy()
    {
        GameObject.FindWithTag("Shooter")?.GetComponent<PlayerShooter>()?.DestroyCallback(gameObject);
        Destroy(gameObject);
    }

    protected virtual void FixedUpdate()
    {
        if (rgbd.position.x <= -20)
            CustomDestroy();
    }

    private void OnBecameInvisible()
    {
        CustomDestroy();
    }
}