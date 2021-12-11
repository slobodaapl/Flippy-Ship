using UnityEngine.UI;

public class UIHealthController : PlayerObserver
{
    private Text goText;

    void Start()
    {
        goText = GetComponent<Text>();
        goText.text = player.health.ToString();
    }
    
    public override void HealthChanged()
    {
        goText.text = player.health.ToString();
    }
}
