using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [Header("Movement properties")]
    public float turnRateDegreesSecond = 60;
    public float maxTurnAngle = 90;
    public float maxAngleSineSpeed = 1;

    [Header("Health and invincibility")]
    public int health = 6; // 2 health = 1 heart, to avoid float usage here
    public float invincibilityDuration = 1f;
    public float invincibilityBlinkFrequency = 3;

    private static bool turningUp;
    private static bool returningToScreen;
    private bool start;

    private bool isInvincible;
    private float remainingInvincibility;
    private int invincibilityBlinkDelayMs;
    
    private float startDelay;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rgbd;

    private List<PlayerObserver> healthObservers;

    public static void SwitchDirection()
    {
        if (!returningToScreen)
            turningUp = !turningUp;
    }

    public void RegisterHealthObserver(PlayerObserver comp)
    {
        healthObservers.Add(comp);
    }

    void Awake()
    {
        healthObservers = new List<PlayerObserver>();
        invincibilityBlinkDelayMs = (int) (invincibilityDuration / (invincibilityBlinkFrequency * 2) * 1000);
        turningUp = false;
        returningToScreen = false;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        startDelay = GameObject.FindWithTag("GameController").GetComponent<PointController>().startDelay;
    }

    void TriggerInvincible()
    {
        remainingInvincibility = invincibilityDuration;
        isInvincible = true;
    }

    void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            healthObservers.ForEach(x => x.HealthChanged());
            TriggerInvincible();
            CheckGameOver();
        }
    }

    void CheckGameOver()
    {
        if (health <= 0)
        {
            PauseController.isPlayerDead = true;
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        var impactableComponent = other.gameObject.GetComponent<Impactable>();
        TakeDamage(impactableComponent.GetDamage());
        impactableComponent.DestroyOnCollission();
    }

    void InvincibilityEffect()
    {
        if (!isInvincible)
            return;

        remainingInvincibility -= Time.fixedDeltaTime;
        
        if (remainingInvincibility <= 0)
        {
            isInvincible = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            return;
        }

        int elapsedMs = (int) ((invincibilityDuration - remainingInvincibility) * 1000);
        float transparency = (elapsedMs % invincibilityBlinkDelayMs) == (elapsedMs % (invincibilityBlinkDelayMs * 2)) ? 1f : 0.5f;
        spriteRenderer.color = new Color(1f, 1f, 1f, transparency);

    }
    
    void AdjustAngle()
    {
        var currentRotation = CalcUtil.ClampAngle(rgbd.rotation);
        var turnDir = turningUp ? 1 : -1;
        var turnAngle = turnRateDegreesSecond * Time.fixedDeltaTime * turnDir;

        if (Mathf.Abs(currentRotation + turnAngle) >= maxTurnAngle)
        {
            rgbd.MoveRotation(Mathf.Sign(currentRotation) * maxTurnAngle);
            return;
        }
        
        rgbd.MoveRotation(turnAngle + currentRotation);
    }
    
    void AdjustPos()
    {
        var angle = CalcUtil.ClampAngle(rgbd.rotation);
        var newOffset = Time.fixedDeltaTime * maxAngleSineSpeed * (angle / maxTurnAngle);
        
        rgbd.MovePosition(rgbd.position + new Vector2(0, newOffset));
    }

    void FixedUpdate()
    {
        if(!start)
            return;
        
        AdjustAngle();
        AdjustPos();
        
        InvincibilityEffect();

        if (returningToScreen && !turningUp)
            turningUp = !turningUp;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad <= startDelay)
            return;
        
        if (!start)
            start = true;

    }

    void OnBecameInvisible()
    {
        TakeDamage(1);
        returningToScreen = true;
        turningUp = !turningUp;
    }

    private void OnBecameVisible()
    {
        returningToScreen = false;
    }
}
