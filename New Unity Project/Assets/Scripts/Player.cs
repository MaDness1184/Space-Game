using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);
    }

    void InitBounds() // Initialize the bounds of the screen at scene start
    {
        Camera mainCamera = Camera.main; // Stores the main camera of the scene in a variable mainCamera
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0.02f, 0.02f));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(0.98f, 0.98f));
    }

    void Movement() // Move Player position with input and keep inside of bounds
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime; // Time.deltaTime = time it took the last frame to render / making movement framerate independent
        Vector2 boundedPos = new Vector2(); // Movement kept in bounds of the screen
        boundedPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x); // bind x movement
        boundedPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y); // bind y movement
        transform.position = boundedPos;
    }
}
