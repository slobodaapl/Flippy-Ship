using UnityEngine;

public class DestroyOnChildless : MonoBehaviour
{
    public float limitX = -15;
    public bool watchXThreshold = false;
    
    void Update()
    {
        if (transform.childCount == 0 || (watchXThreshold && transform.GetChild(0).position.x <= limitX))
            Destroy(gameObject);
    }
}