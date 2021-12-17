using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private void Start() // Just deactivate on beginning
    {
        gameObject.SetActive(false);
    }
}