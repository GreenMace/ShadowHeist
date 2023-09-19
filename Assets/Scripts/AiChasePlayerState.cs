using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public Transform playerTransform;
    public float movementSpeed = 3;

    public AiStateId GetId() {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent) {
        if (playerTransform == null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        agent.movementController.maxSpeed = movementSpeed;
    }

    public void Update(AiAgent agent) {
        agent.ai.destination = playerTransform.position;
    }

    public void Exit(AiAgent agent) {

    }
}
