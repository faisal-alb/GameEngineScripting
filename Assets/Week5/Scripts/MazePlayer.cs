using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazePlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;
    private Rigidbody rb;

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
}
