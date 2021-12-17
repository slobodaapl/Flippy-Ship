using UnityEngine;

public abstract class BaseEnemyProjectile : Impactable
{
    protected virtual void FixedUpdate()
    {
        if (rgbd.position.x <= -20) // Destroy it if it never went on screen and goes too far left
            CustomDestroy();
    }

    private void OnBecameInvisible()
    {
        CustomDestroy();
    }

    public void InitOffset(float offset)
    {
        rgbd.MovePosition(rgbd.position + new Vector2(offset, 0));
    }

    private void CustomDestroy() // If projectile is shootable, we remove it from tracked shootable objects
    {
        GameObject.FindWithTag("Shooter")?.GetComponent<PlayerShooter>()?.DestroyCallback(gameObject);
        Destroy(gameObject);
    }
}