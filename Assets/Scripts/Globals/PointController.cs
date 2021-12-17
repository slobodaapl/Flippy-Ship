using UnityEngine;

public class PointController : PlayerObserver
{
    public GameObject uiMultiplier; // The multiplier gui object
    public GameObject uiScore; // Score gui object

    public float baseTimeUntilNextMultiplier = 5; // How long the player must survive without a hit to get next mult. lvl
    public float baseScoreIncement = 1; // How much score the player gets per fixedUpdate increment
    public float baseDestructionScoreWorth = 100; // How much is one destruction point worth before multiplier

    public float startDelay = 1; // How much grace period the player is given on the beginning before they start falling
    private int destruction; // acquired destruction pts
    private int prevDestruction; // previously acquired destruction pts, before counted towards score
    private int multiplier = 1; // score multiplier
    private double score;

    private UIMultiplierController multiplierController; // Controller for the UI multiplier
    private UIScoreController scoreController; // How much score the player has

    private float timeUntilNextMultiplier; // This gets bigger with every multiplier, based on the baseTimeUntilNextMult

    private void Start()
    {
        multiplierController = uiMultiplier.GetComponent<UIMultiplierController>();
        scoreController = uiScore.GetComponent<UIScoreController>();
        GameObject.FindWithTag("Player").GetComponent<PlayerShip>().RegisterHealthObserver(this);
        timeUntilNextMultiplier = baseTimeUntilNextMultiplier;
    }

    private void Update()
    {
        scoreController.UpdateScore(score, multiplier); // Update the UI every frame
    }

    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad <= startDelay) // Start counting score after grace period ends
            return;

        ScaleMultiplier(); // Calculate if player reached next multiplier, and update UI, and update next mult. threshold
        BankDestruction(); // Add newly acquired destruction points to score

        score += baseScoreIncement * multiplier;
    }

    public double GetScore()
    {
        return score;
    }

    public override void HealthChanged(bool damage) // Observer, triggered by player when health changes
    {
        if (!damage) return;

        multiplier = 1;
        multiplierController.UpdateMultplier(1);
        timeUntilNextMultiplier = baseTimeUntilNextMultiplier;
    }

    public void AddDestruction(int pts)
    {
        destruction += pts;
    }

    private void ScaleMultiplier()
    {
        timeUntilNextMultiplier -= Time.fixedDeltaTime;
        if (timeUntilNextMultiplier > 0) return;

        multiplier = Mathf.Clamp(multiplier + 1, 1, 5);
        timeUntilNextMultiplier = baseTimeUntilNextMultiplier * Mathf.Pow(2, multiplier - 1);
        multiplierController.UpdateMultplier(multiplier);
    }

    private void BankDestruction() // Turn acquired destruction points into score
    {
        if (prevDestruction == destruction) return;

        score += (destruction - prevDestruction) * baseDestructionScoreWorth * multiplier;
        prevDestruction = destruction;
    }
}