using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    
    private Vector2 movement;
    public bool crouching = false;
    private Rigidbody2D rb;
    private GameObject spriteRenderer;
    
    [SerializeField] private float normalSpeed = 4;
    private float activeSpeed;
    public float soundRadius = 1;
    [SerializeField] private float changeSoundRadius = 1;
    [SerializeField] private float sneakMultiplyer = 0.8f;
    private bool isSneaking = false;

    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find("Sprite").gameObject;
    }

    public void OnMovement(InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2>();
        
    }

    private void FixedUpdate() {
        if (isSneaking)
        {
            activeSpeed = normalSpeed * sneakMultiplyer;
        }
        else
        {
            activeSpeed = normalSpeed;
        }
        rb.MovePosition(rb.position + movement * activeSpeed * Time.fixedDeltaTime);

        if (movement != Vector2.zero) {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            spriteRenderer.transform.rotation = toRotation;
        }
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
