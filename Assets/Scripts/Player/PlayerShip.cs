using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private static bool turningUp;
    private static bool returningToScreen;

    [Header("Movement properties")] [Range(0, 90)]
    public float turnRateDegreesSecond = 60;

    [Range(0, 60)] public float maxTurnAngle = 60;

    [Range(5, 15)] public float maxAngleSineSpeed = 10; // The multiplier of how fast we move vertically based on angle

    [Header("Health and invincibility")] [Min(1)]
    public int maxHealth = 3; // Max health for if the player heals up

    [Min(0)] public int health = 3; // Starting health. Must be smalller or equal than maxHealth

    [Min(0)] public float invincibilityDuration = 1f; // How long the player stays invincible when hit

    [Range(2, 16)] public float invincibilityBlinkFrequency = 3; // How many times the player blinks per second to signify invincibility. Just visual

    private List<PlayerObserver> healthObservers; // List of objects subscribed to watch the player's health
    private int invincibilityBlinkDelayMs; // Calculated based on blink frequency

    private bool isInvincible;
    private float remainingInvincibility; // Time left till end of invincibility
    private Rigidbody2D rgbd;

    private SpriteRenderer spriteRenderer; // To do the invincibility effect using transparency
    private bool start;

    private float startDelay;

    private void OnValidate() // Fix health if larger than maxHealth in Inspector
    {
        if (health > maxHealth)
            health = maxHealth;
    }

    private void Awake()
    {
        healthObservers = new List<PlayerObserver>();
        invincibilityBlinkDelayMs = (int)(invincibilityDuration / (invincibilityBlinkFrequency * 2) * 1000);
        health = maxHealth;
        turningUp = false;
        returningToScreen = false;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        startDelay = GameObject.FindWithTag("GameController").GetComponent<PointController>().startDelay;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad <= startDelay)
            return;

        if (!start)
            start = true;
    }

    private void FixedUpdate()
    {
        if (!start)
            return;

        AdjustAngle(); // Adjust the angle, which is then used to calculate vertical difference
        AdjustPos(); // Move the rigidbody up or down based on the rate of the current angle to max angle.
        
        // (No Mathf.Sin were hurt in the making of this class)

        InvincibilityEffect(); 

        if (returningToScreen) // Validate the player didn't accidentaly click the moment that OnBecameInvisible triggered
        {
            var sign = Mathf.Sign(rgbd.position.y);
            if (sign > 0 && turningUp || sign < 0 && !turningUp)
                turningUp = !turningUp;
        }
    }

    private void OnBecameInvisible() // If player flies off screen, damage him, and force him to return
    {
        TakeDamage(1);
        returningToScreen = true; // This makes it impossible to click while ship is returning
        turningUp = !turningUp;
    }

    private void OnBecameVisible()
    {
        returningToScreen = false;
    }

    private void OnCollisionEnter2D(Collision2D other) // Take damage based on other object's damage value, and destroy it if possible
    {
        var impactableComponent = other.gameObject.GetComponent<Impactable>();
        TakeDamage(impactableComponent.GetDamage());
        impactableComponent.DestroyOnCollission();
    }

    public static void SwitchDirection() // Makes it possible to switch the direction externally
    {
        if (!returningToScreen)
            turningUp = !turningUp;
    }

    public void RegisterHealthObserver(PlayerObserver comp) // Add an observer
    {
        healthObservers.Add(comp);
    }

    public void NotifyHealthObserver(bool damage) // Tell observers that hp changed, and if it was due to damage
    {
        healthObservers.ForEach(x => x.HealthChanged(damage));
    }

    public Vector2 Get2DPos() // Get the ship's position externally
    {
        if (rgbd != null)
            return rgbd.position;

        return Vector2.zero;
    }

    public void TriggerInvincible(float duration) // Make ship invincible with set duration. Called when player gets an upgrade
    {
        remainingInvincibility = duration;
        isInvincible = true;
    }

    private void TriggerInvincible() // Internal invincibiliy trigger for when player takes dmg
    {
        remainingInvincibility = invincibilityDuration;
        isInvincible = true;
    }

    private void TakeDamage(int damage) // If not invincible, take dmg and check if dead. Also triggers invincibility .. 'i-frames'
    {
        if (!isInvincible)
        {
            health -= damage;
            NotifyHealthObserver(true);
            TriggerInvincible();
            CheckGameOver();
        }
    }

    private void CheckGameOver()
    {
        if (health <= 0)
        {
            PauseController.isPlayerDead = true; // Will show pause menu with game over
            Destroy(gameObject);
        }
    }

    private void InvincibilityEffect() // Blinking effect for invincibility
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

        var elapsedMs = (int)((invincibilityDuration - remainingInvincibility) * 1000);
        var transparency = elapsedMs % invincibilityBlinkDelayMs == elapsedMs % (invincibilityBlinkDelayMs * 2)
            ? 1f
            : 0.5f;
        spriteRenderer.color = new Color(1f, 1f, 1f, transparency);
    }

    private void AdjustAngle() // See FixedUpdate
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

    private void AdjustPos() // See FixedUpdate
    {
        var angle = CalcUtil.ClampAngle(rgbd.rotation);
        var newOffset = Time.fixedDeltaTime * maxAngleSineSpeed * (angle / maxTurnAngle);

        rgbd.MovePosition(rgbd.position + new Vector2(0, newOffset));
    }
}