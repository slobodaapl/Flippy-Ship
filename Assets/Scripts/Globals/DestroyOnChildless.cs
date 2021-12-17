using UnityEngine;

public class DestroyOnChildless : MonoBehaviour
{
    public float limitX = -15;
    
    // Little patch for objects that never appear on the screen, so they don't get destroyed by OnBecameInvisible
    public bool watchXThreshold; 

    private void Update() // Script that destroys an 'empty' prefab holder (for arrangements of enemies) when it has no enemies inside left
    {
        if (transform.childCount == 0 || (watchXThreshold && transform.GetChild(0).position.x <= limitX))
            Destroy(gameObject);
    }
}