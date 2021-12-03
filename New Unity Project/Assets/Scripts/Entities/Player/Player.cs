using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    normal,
    flash
}

public class Player : MonoBehaviour
{
    [Header("Player State")]
    public PlayerState currentState;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Abilities")]
    public float dashDistanceMult = 2f; 

    // private
    Vector2 playerPosition;
    Vector2 rawInput; // The raw input value of move key
    Vector2 minBounds; // minimum bounds of the camera 
    Vector2 maxBounds; // maximum bounds of the camera
    Shooter shooter; // Gets Shooter component of Player

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        currentState = PlayerState.normal;
        playerPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerPosition);
        if (CheckState(PlayerState.normal))
        {
            Movement();
        }
    }

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

    void OnMove(InputValue value) // Called whenever Player inputs Move
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value) // Called whenever Player inputs Fire
    {
        if (shooter != null && CheckState(PlayerState.normal)) // Checking if the Shooter script is attached to our object
        {
            shooter.isFireing = value.isPressed;
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
            Debug.Log("Dash");
        }
    }

    void InitBounds() // Initialize the bounds of the screen at scene start
    {
        Camera mainCamera = Camera.main; // Stores the main camera of the scene in a variable mainCamera
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0.02f, 0.02f)); // Converts Camera Position to World Position (Bottom Left)
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(0.98f, 0.98f)); // Converts Camera Position to World Position (Top Right)
    }

    void Movement() // Move Player position with input and keep inside of bounds
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime; // Time.deltaTime = time it took the last frame to render / making movement framerate independent

        playerPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x); // bind x movement
        playerPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y); // bind y movement
 
        transform.position = playerPosition;
    }
}
