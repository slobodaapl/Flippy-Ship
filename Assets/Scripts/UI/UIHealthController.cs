using UnityEngine.UI;

public class UIHealthController : PlayerObserver // Used to update the HP UI when ship hp changes
{
    private Text goText;

    private void Start()
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