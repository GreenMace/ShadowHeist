using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    
    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField] private int speed = 5;
    public float soundRadius = 1;
    public float changeSoundRadius = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMovement(InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2>();
        
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    public void OnSneak(InputAction.CallbackContext context)
    {    
        if (context.started)
        {
            soundRadius = 0;
        }
        if (context.canceled)
        {
            soundRadius = changeSoundRadius;
        }
    }
}
