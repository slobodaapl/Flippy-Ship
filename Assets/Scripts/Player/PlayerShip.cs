using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [Header("Movement properties")]
    [Range(0, 90)]
    public float turnRateDegreesSecond = 60;
    [Range(0, 60)]
    public float maxTurnAngle = 60;
    [Range(0.5f, 2)]
    public float maxAngleSineSpeed = 1;

    [Header("Health and invincibility")]
    [Min(1)]
    public int maxHealth = 3;
    [Min(0)]
    public int health = 3;
    [Min(0)]
    public float invincibilityDuration = 1f;
    [Range(2, 16)]
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

    public void NotifyHealthObserver(bool damage)
    {
        healthObservers.ForEach(x => x.HealthChanged(damage));
    }

    void Awake()
    {
        healthObservers = new List<PlayerObserver>();
        invincibilityBlinkDelayMs = (int) (invincibilityDuration / (invincibilityBlinkFrequency * 2) * 1000);
        health = maxHealth;
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
            NotifyHealthObserver(true);
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
