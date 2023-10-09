using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    private Vector2 movement;
    public bool crouching = false;
    private Rigidbody2D rb;
    public float speed = 1f;

    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMovement(InputAction.CallbackContext context) {
        movement = context.ReadValue<Vector2>();

    }

    public void OnCrouch(InputAction.CallbackContext context) {
        if (context.performed) {
            crouching = true;
        } else if (context.canceled) {
            crouching = false;
        }
    }

    public void Update() {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
