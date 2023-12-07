using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtPlayerScript : MonoBehaviour
{
    public LevelHandlerScript levelHandler;

    // Start is called before the first frame update
    void Start() {
        levelHandler = GameObject.FindGameObjectWithTag("Level Handler").GetComponent<LevelHandlerScript>(); ;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            levelHandler.GameOver();
        }
    }
}
