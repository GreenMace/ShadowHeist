using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public LevelHandlerScript levelHandler;
    public Transform respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = GameObject.FindGameObjectWithTag("Level Handler").gameObject.GetComponent<LevelHandlerScript>();        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            levelHandler.SetRespawnPoint(respawnPosition);
        }
    }
}
