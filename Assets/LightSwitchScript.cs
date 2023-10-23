using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LightSwitchScript : MonoBehaviour
{
    public bool CanInteract = false;
    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = transform.parent.gameObject.GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player") == true) {
            CanInteract = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player") == true) {
            CanInteract = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }
    }
    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed && CanInteract) {
            light.enabled = !light.enabled;

        }
    }
}
