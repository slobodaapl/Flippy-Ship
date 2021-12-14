
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class UIMultiplierController : MonoBehaviour
{
    private Text goText;

    void Start()
    {
        goText = GetComponent<Text>();
    }
    
    public void UpdateMultplier(int multiplier)
    {
        if (goText != null)
            goText.text = $"{multiplier}x Multiplier";
    }
}