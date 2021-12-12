using UnityEngine;

public class PointController : PlayerObserver
{
    public GameObject uiMultiplier;
    public GameObject uiScore;

    public float baseTimeUntilNextMultiplier = 5;
    public float baseScoreIncement = 1;
    public float baseDestructionScoreWorth = 100;

    public float startDelay = 1;

    private UIMultiplierController multiplierController;
    private UIScoreController scoreController;

    private double score;
    private int destruction;
    private int prevDestruction;
    private int multiplier = 1;

    private float timeUntilNextMultiplier;

    void Start()
    {
        multiplierController = uiMultiplier.GetComponent<UIMultiplierController>();
        scoreController = uiScore.GetComponent<UIScoreController>();
        GameObject.FindWithTag("Player").GetComponent<PlayerShip>().RegisterHealthObserver(this);
        timeUntilNextMultiplier = baseTimeUntilNextMultiplier;
    }
    
    public override void HealthChanged()
    {
        multiplier = 1;
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

    private void BankDestruction()
    {
        if (prevDestruction == destruction) return;
        
        score += (destruction - prevDestruction) * baseDestructionScoreWorth * multiplier;
        prevDestruction = destruction;
    }
    
    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad <= startDelay)
            return;

        ScaleMultiplier();
        BankDestruction();
        
        score += baseScoreIncement * multiplier;
    }

    void Update()
    {
        scoreController.UpdateScore(score, multiplier);
    }
}