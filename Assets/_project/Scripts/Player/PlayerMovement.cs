using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 input;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   ;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = input * speed;
    }

    public void Move(InputAction.CallbackContext context)
    {
       input = context.ReadValue<Vector2>();
    }
}
