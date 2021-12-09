using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShip : MonoBehaviour
{
    [Header("Movement properties")]
    public float startDelay = 1;
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

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rgbd;

    public static void SwitchDirection()
    {
        if (!returningToScreen)
            turningUp = !turningUp;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        invincibilityBlinkDelayMs = (int) (invincibilityDuration / (invincibilityBlinkFrequency * 2) * 1000);
    }

    void TriggerInvincible()
    {
        if(isInvincible)
            return;
        
        remainingInvincibility = invincibilityDuration;
        isInvincible = true;
    }

    void TakeDamage(int damage)
    {
        health -= isInvincible ? 0 : damage;
        TriggerInvincible();
    }

    void CheckGameOver()
    {
        if (health <= 0)
        {
            // TODO endgame handling
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (isInvincible)
            return;
        
        var impactableComponent = other.gameObject.GetComponent<Impactable>();
        TakeDamage(impactableComponent.GetDamage());
        impactableComponent.DestroyOnCollission();
        CheckGameOver();
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
    }

    void Update()
    {
        if (Time.time <= startDelay)
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
