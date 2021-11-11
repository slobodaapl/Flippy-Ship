using System;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [Header("Movement properties")]
    public float startDelay = 1;
    public float turnRateDegreesSecond = 60;
    public float maxTurnAngle = 90;
    public float maxAngleSineSpeed = 1;

    [Header("Health and invincibility")]
    [Range(1, 20)]
    public int health = 6; // 2 health = 1 heart, to avoid float usage here
    public float invincibilityDuration = 1f;
    public float invincibilityBlinkFrequency = 3;

    private bool start = false;
    private bool turningUp = false;
    private float currentRotation = 0;
    
    private bool isInvincible = false;
    private float remainingInvincibility;
    private int invincibilityBlinkDelayMs;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        invincibilityBlinkDelayMs = (int) (invincibilityDuration / (invincibilityBlinkFrequency * 2)) * 1000;
    }

    void TriggerInvincible()
    {
        remainingInvincibility = invincibilityDuration;
        isInvincible = true;
    }
    
    void CheckGameOver()
    {
        if (health <= 0)
        {
            // TODO endgame handling
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Impactable obj = other.gameObject.GetComponent<Impactable>();
        health -= obj.collisionDamage;
        CheckGameOver();
        TriggerInvincible();
    }

    void AdjustAngle()
    {
        if(!start)
            return;
        
        var turnDir = turningUp ? 1 : -1;
        var turnAngle = turnRateDegreesSecond * Time.fixedDeltaTime * turnDir;

        var finalAngle = transform.eulerAngles + Vector3.forward * turnAngle;

        if (Mathf.Abs(currentRotation + turnAngle) >= maxTurnAngle)
        {
            currentRotation = turnDir * maxTurnAngle;
            finalAngle = Vector3.forward * currentRotation;
        }
        else
        {
            currentRotation += turnAngle;
        }
        
        transform.eulerAngles = finalAngle;
    }

    void AdjustPos()
    {
        var currentPos = transform.position;
        currentPos.y += maxAngleSineSpeed * (currentRotation / maxTurnAngle);
        transform.position = currentPos;
    }

    void InvincibilityEffect()
    {
        if (!isInvincible)
            return;

        remainingInvincibility -= Time.deltaTime;
        
        if (remainingInvincibility <= 0)
        {
            isInvincible = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            return;
        }

        int elapsedMs = (int) (invincibilityDuration - remainingInvincibility) * 1000;
        float transparency = (elapsedMs % invincibilityBlinkDelayMs) == (elapsedMs % (invincibilityBlinkDelayMs * 2)) ? 1f : 0.5f;
        spriteRenderer.color = new Color(1f, 1f, 1f, transparency);

    }
    void FixedUpdate()
    {
        AdjustAngle();
        AdjustPos();
    }

    void Update()
    {
        if (Time.time <= startDelay)
            return;
        
        if (!start)
            start = true;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            turningUp = !turningUp;
        
        InvincibilityEffect();

    }

    void OnBecameInvisible()
    {
        Destroy(this);
        Application.Quit();
    }
}
