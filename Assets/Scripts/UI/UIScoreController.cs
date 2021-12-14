using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
    private Text goText;

    void Start()
    {
        goText = GetComponent<Text>();
    }
    
    public void UpdateScore(double score, int multiplier)
    {
        if (goText != null)
        {
            goText.text = $"{(ulong)score}"; // Rounding down by cutting off fraction to a unsigned long, cause double
            goText.color = MultiplierColor.GetColor(multiplier);
        }
    }
}
