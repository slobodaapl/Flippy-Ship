using UnityEngine.UI;

public class UIHealthController : PlayerObserver
{
    private Text goText;

    void Start()
    {
        goText = GetComponent<Text>();
        goText.text = $"HP: {player.health.ToString()}";
        player.RegisterHealthObserver(this);
    }
    
    public override void HealthChanged(bool damage)
    {
        if (goText != null)
            goText.text = $"HP: {player.health.ToString()}";
    }
}
