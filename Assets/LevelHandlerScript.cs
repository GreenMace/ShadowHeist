using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandlerScript : MonoBehaviour
{
    public Transform startPoint;
    Transform respawnPoint;

    public GameObject player;
    Rigidbody2D playerTransform;

    // Start is called before the first frame update
    void Start() {
        respawnPoint = startPoint;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Rigidbody2D>();
        Spawn();
    }

    // Update is called once per frame
    void Update() {

    }

    public void GameOver() {
        Respawn();
    }

    public void Restart() {
        SceneManager.LoadScene(1);
    }
    
    public void Spawn() {
        playerTransform.position = respawnPoint.position;
        playerTransform.rotation = respawnPoint.rotation.z;
    }

    public void Respawn() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            AiAgent enemyAI = enemy.transform.GetComponent<AiAgent>();
            enemyAI.reset();
        }

        playerTransform.position = respawnPoint.position;
        playerTransform.rotation = respawnPoint.rotation.z;
    }

    public void LevelComplete() {

    }

    public void SetRespawnPoint(Transform newRespawnPoint) {
        respawnPoint = newRespawnPoint;
    }

}
