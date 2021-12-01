using UnityEngine;

public class DestroyOnChildless : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount == 0)
            Destroy(gameObject);
    }
}