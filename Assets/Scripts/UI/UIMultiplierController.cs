using UnityEngine;
using UnityEngine.UI;

public class UIMultiplierController : MonoBehaviour // Change multiplier UI on demand.. Rocking that MVC pattern
{
    private Text goText;

    private void Start()
    {
        goText = GetComponent<Text>();
    }

    public void UpdateMultplier(int multiplier)
    {
        if (goText != null)
            goText.text = $"{multiplier}x Multiplier";
    }
}