using UnityEngine;

public class PlainMove : MonoBehaviour // Just something that plainly moves and gets destroyed offscreen
{
    private float defaultUnitSpeed;

    private void Update()
    {
        if (transform.position.x <= -17)
            Destroy(gameObject);

        var pos = transform.position;
        transform.position =
            pos + new Vector3(-TimeTracker.GetMoveMultiplier() * defaultUnitSpeed * Time.deltaTime, 0, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetDefaultUnitSpeed(float speed)
    {
        defaultUnitSpeed = speed;
    }
}