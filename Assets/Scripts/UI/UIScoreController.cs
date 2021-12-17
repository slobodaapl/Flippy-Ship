using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour // Update score on demand on each frame, called from PointController
{
    private Text goText;

    private void Start()
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