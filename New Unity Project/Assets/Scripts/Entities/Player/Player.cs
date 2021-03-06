using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    normal,
    dash
}

public class Player : MonoBehaviour
{
    #region Variables

    [Header("Player State")]
    [SerializeField] PlayerState currentState;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Ability Batterys")]
    [SerializeField] FloatReference playerBatteries;
    [SerializeField] SignalSender playerBatterySignal;
    [SerializeField] int dashBatteryUsage = 1;

    [Header("Dash")]
    [SerializeField] float dashSpeedMult = 50f;
    [SerializeField] float dashMovementDelay = 0.5f;
    [SerializeField] float dashInvulnerabilityTime = 0.6f;
    [SerializeField] float dashCooldown = 0.5f;
    [SerializeField] float dashShakeMagnitude = 0.3f;
    [SerializeField] float dashShakeDuration = 0.1f;
    private float timeTillNextDash = 0.0f;

    // private
    Vector2 playerVelocity;
    Vector2 playerInput; // The raw input value of move key
    Vector2 minBounds; // minimum bounds of the camera 
    Vector2 maxBounds; // maximum bounds of the camera

    Shooter myShooter; // Gets Shooter component of Player
    DamageDealer myDamageDealer;
    PlayerHealth myHealth;
    Rigidbody2D myRigidbody2D;
    CameraShake cameraShake;
    #endregion

    void Awake()
    {
        myShooter = GetComponent<Shooter>();
        myDamageDealer = GetComponent<DamageDealer>();
        myHealth = GetComponent<PlayerHealth>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.normal;
        myDamageDealer.SetDamageEnabler(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckState(PlayerState.normal))
        {
            Movement();
        }
    }

    Vector2 GetPlayerPosition()
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        return playerPosition;
    }

    public float GetAbilityBatteries()
    {
        return playerBatteries.GetRuntimeValue();
    }

    public void UpdateAbilityBatteries(int amount)
    {
        if (playerBatteries.GetRuntimeValue() > 0f)
            playerBatteries.SubtractRuntimeValue(amount);
    }

    #region PlayerState

    public void ChangeState(PlayerState newState)
    {
        if (!CheckState(newState))
        {
            currentState = newState;
        }
    }

    public void ChangeState(PlayerState newState, PlayerState oldState)
    {
        if (!CheckState(oldState))
        {
            currentState = newState;
        }
    }

    public bool CheckState(PlayerState state) // Compares currentState with param.
    {
        return currentState == state;
    }
    #endregion

    #region InputActions
    void OnMove(InputValue value) // Called whenever Player inputs Move
    {
        playerInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value) // Called whenever Player inputs Fire
    {
        if (myShooter != null && CheckState(PlayerState.normal)) // Checking if the Shooter script is attached to our object
        {
            myShooter.isFireing = value.isPressed;
        }
    }

    void OnRotateLeft(InputValue value)
    {
        if (value.isPressed && CheckState(PlayerState.normal))
        {
            transform.Rotate(0.0f, 0.0f, 90.0f);
        }
    }

    void OnRotateRight(InputValue value)
    {
        if (value.isPressed && CheckState(PlayerState.normal))
        {
            transform.Rotate(0.0f, 0.0f, -90.0f);
        }
    }

    void OnDash(InputValue value)
    {
        if(value.isPressed && CheckState(PlayerState.normal))
        {
            Dash();
        }
    }
    #endregion

    #region Actions
    void Movement() // Move Player position with input and keep inside of bounds
    {
        playerVelocity = playerInput * moveSpeed * Time.fixedDeltaTime; // Time.deltaTime = time it took the last frame to render / making movement framerate independent
        myRigidbody2D.MovePosition(GetPlayerPosition() + playerVelocity);
    }

    void Dash()
    {
        if (CheckCooldown(PlayerState.dash) && GetAbilityBatteries() > 0)
        {
            StartCoroutine(DashCo());
            Vector2 force = playerInput * dashSpeedMult;
            myRigidbody2D.AddForce(force, ForceMode2D.Impulse);
            ShakeCamera();

            playerBatterySignal.Raise();
            UpdateAbilityBatteries(dashBatteryUsage); // Update Batteries

            StartCooldown(PlayerState.dash);
        }
    }

    void ShakeCamera() // Shake camera when gameObject
    {
        if (cameraShake != null)
        {
            cameraShake.PlayCameraShake(dashShakeMagnitude, dashShakeDuration);
        }
    }
    #endregion

    #region Cooldowns
    void StartCooldown(PlayerState cooldownType)
    {
        if (cooldownType == PlayerState.dash)
        {
            timeTillNextDash = Time.time + dashCooldown;
        }
    }

    bool CheckCooldown(PlayerState cooldownType)
    {
        if (cooldownType == PlayerState.dash)
        {
            if (Time.time > timeTillNextDash)
                return true;
            else
                return false;
        }
        Debug.Log("Invalid Cooldown Type");
        return false;
    }
    #endregion Cooldowns

    #region Coroutines
    IEnumerator DashCo()
    {
        myHealth.Invulnerable(dashInvulnerabilityTime);
        myDamageDealer.SetDamageEnabler(true);
        ChangeState(PlayerState.dash);
        yield return new WaitForSeconds(dashMovementDelay);
        myDamageDealer.SetDamageEnabler(false);
        myRigidbody2D.velocity = Vector2.zero;
        ChangeState(PlayerState.normal);
    }
    #endregion
}
