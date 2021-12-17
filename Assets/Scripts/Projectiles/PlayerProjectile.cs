using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float durationVisible;
    private GameObject ship;

    private Vector3 targetPos;

    private void Start()
    {
        ship = GameObject.FindWithTag("Player");
    }

    private void Update() // The weird wiggly player lazer. Doesn't track the object well, tried debugging, idk how to fix
    {
        if (ship == null)
        {
            Destroy(gameObject);
            return;
        }

        if (gameObject != null && durationVisible <= 0)
        {
            Destroy(gameObject);
            return;
        }

        durationVisible -= Time.deltaTime;

        targetPos -= new Vector3(Time.deltaTime * 5.7f, 0, 0);
        var shipPos = ship.transform.position;
        var targetVec = targetPos - shipPos;
        var forwardVec = ship.transform.right;
        var angle = Vector3.SignedAngle(targetVec, forwardVec, Vector3.up);
        var scale = Vector3.Distance(gameObject.transform.position, targetPos);
        gameObject.transform.localScale = new Vector3(scale, 1, 1);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        gameObject.transform.position = shipPos;
    }

    public void Init(Vector3 target)
    {
        targetPos = target;
    }
}