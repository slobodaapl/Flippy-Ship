using UnityEngine.UI;

public class UIHealthController : PlayerObserver
{
    private Text goText;

    void Start()
    {
        goText = GetComponent<Text>();
        goText.text = $"Health: {player.health.ToString()}";
        player.RegisterHealthObserver(this);
    }
    
    public override void HealthChanged()
    {
        if (goText != null)
            goText.text = $"Health: {player.health.ToString()}";
    }
}
