using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class TurnOnLight : MonoBehaviour
{
    public bool CanInteract = false;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") == true)
        {
            CanInteract = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") == true)
        {
            CanInteract = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && CanInteract)
        {
            GetComponent<Light2D>().enabled = !GetComponent<Light2D>().enabled;
            
        }
    }
}
