using System;
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
    [Range(1, 20)]
    public int health = 6; // 2 health = 1 heart, to avoid float usage here
    public float invincibilityDuration = 1f;
    public float invincibilityBlinkFrequency = 3;

    private bool start = false;
    private bool turningUp = false;
    private float posYOffset;
    private float angleOffset;

    private bool isInvincible = false;
    private float remainingInvincibility;
    private int invincibilityBlinkDelayMs;

    private SpriteRenderer spriteRenderer;
    private int debug = 0;

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

    private static float ClampAngle(float unadjusted)
    {
        return unadjusted > 180 ? unadjusted - 360 : unadjusted;
    }
    void AdjustAngle()
    {
        var currentRotation = transform.eulerAngles;
        var turnDir = turningUp ? 1 : -1;
        var turnAngle = turnRateDegreesSecond * Time.fixedDeltaTime * turnDir;
        angleOffset += turnAngle;
        
        var currentAngle = ClampAngle(Vector3.Dot(currentRotation, Vector3.forward));

        if (Mathf.Abs(currentAngle + angleOffset) >= maxTurnAngle)
            angleOffset = Mathf.Sign(currentAngle) * maxTurnAngle - currentAngle;
        
    }

    void AdjustPos()
    {
        var angle = ClampAngle(Vector3.Dot(Vector3.forward, transform.eulerAngles));
        posYOffset += Time.fixedDeltaTime * maxAngleSineSpeed * (angle / maxTurnAngle);
    }
    
    private bool MouseOverUIElement =>
        EventSystem.current.currentSelectedGameObject != null &&
        EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("UI");

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
        if(!start)
            return;
        
        AdjustAngle();
        AdjustPos();
    }

    void TransformPlayer()
    {
        transform.Translate(0, posYOffset, 0, Space.World);
        transform.Rotate(angleOffset * Vector3.forward);

        posYOffset = 0;
        angleOffset = 0;
    }

    void Update()
    {
        if (Time.time <= startDelay)
            return;
        
        if (!start)
            start = true;

        TransformPlayer();

        if (!PauseController.IsPaused)
        {
            if (Input.GetMouseButtonDown(0))
                if (!MouseOverUIElement)
                    turningUp = !turningUp;
        }

        InvincibilityEffect();

    }

    void OnBecameInvisible()
    {
        //Debug.Log("Now invisible");
        //Destroy(this);
    }
}
