using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MazePlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private TextMeshProUGUI healthText;
    private InputAction moveAction;
    private Rigidbody rb;

    private int health = 100;

    private void Awake()
    {
        moveAction = inputActions.FindActionMap("MazeActionMap").FindAction("Move");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();

        float xInput = moveVector.x * moveSpeed;
        float yInput = moveVector.y * moveSpeed;
        
        rb.velocity = new Vector3(xInput, 0.0f, yInput);
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("MazeActionMap").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("MazeActionMap").Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MazeLava"))
        {
            DecreaseHealth(10);
        }
    }

    private void DecreaseHealth(int amount)
    {
        health -= amount;

        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }

        if (health <= 0)
        {
            // reset game.
            // Reset System Incomplete
        }
    }
}
