using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class OpenDoor : MonoBehaviour
{
    public bool CanInteract = false;
    SpriteSwitchingScript spriteSwitcher;

    void Start() {
        spriteSwitcher = GetComponent<SpriteSwitchingScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
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
            gameObject.GetComponent<BoxCollider2D>().enabled = !gameObject.GetComponent<BoxCollider2D>().enabled;
            //gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            spriteSwitcher.NextSprite();
        }
    }
}
