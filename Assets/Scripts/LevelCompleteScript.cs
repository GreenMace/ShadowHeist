using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelCompleteScript : MonoBehaviour
{
    private LevelHandlerScript levelHandler;
    private GameObject finishText;
    private bool canFinish = false;

    void Start () {
        levelHandler = GameObject.FindGameObjectWithTag("Level Handler").GetComponent<LevelHandlerScript>();
        finishText = GameObject.FindGameObjectWithTag("Player").gameObject.transform.Find("Player UI").gameObject.transform.Find("Finish Level Text").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "Player") {
            finishText.SetActive(true);
            canFinish = true;
        }
    }

    private void OnTriggerExit2D(Collider2D c) {
        if (c.tag == "Player") {
            finishText.SetActive(false);
            canFinish = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed && canFinish) {
            levelHandler.LevelComplete();
        }
    }
}
