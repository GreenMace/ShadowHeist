using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    
    private Vector2 movement;
    private Rigidbody2D rb;
    [SerializeField] private float normalSpeed = 5;
    private float activeSpeed = 5;
    public float soundRadius = 1;
    [SerializeField] private float changeSoundRadius = 1;
    [SerializeField] private float sneackMultiplyer = 0.8f;
    private bool isSneaking = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMovement(InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2>();
        
    }

    private void FixedUpdate() {
        if (isSneaking)
        {
            activeSpeed = normalSpeed * sneackMultiplyer;
        }
        else
        {
            activeSpeed = normalSpeed;
        }
        rb.MovePosition(rb.position + movement * activeSpeed * Time.fixedDeltaTime);
    }
    public void OnSneak(InputAction.CallbackContext context)
    {    
        if (context.started)
        {
            isSneaking = true;
            soundRadius = 0.1f;
        }
        if (context.canceled)
        {
            isSneaking = false;
            soundRadius = changeSoundRadius;
        }
    }
}
