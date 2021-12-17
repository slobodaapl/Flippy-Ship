using UnityEngine;

public class RedProjectile : BaseEnemyProjectile
{
    public void InitAngle(float angle) // Just moves under a certain angle based on initialization from RedEnemyShip
    {
        angle *= Mathf.Deg2Rad;
        rgbd.velocity = defaultUnitSpeed * new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle));
    }
}