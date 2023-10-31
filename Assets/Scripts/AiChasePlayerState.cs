using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public Transform playerTransform;
    public float movementSpeed = 3;

    float playerLastSeenTime;
    float chaseTime = 3;

    public AiStateId GetId() {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent) {

        if (playerTransform == null) {
            playerTransform = agent.player.transform;
        }

        agent.movementController.maxSpeed = movementSpeed;
    }

    public void Update(AiAgent agent) {
        if (Vector3.Distance(playerTransform.position, agent.transform.position) < 3 && agent.FoV.canSeePlayer) {
            agent.stateMachine.ChangeState(AiStateId.Tackle);
        }

        agent.pathfinder.destination = playerTransform.position;
        
        if (agent.FoV.canSeePlayer || agent.SoundDetect.canHearPlayer) {
            playerLastSeenTime = Time.time;
        }

        if (Time.time - chaseTime > playerLastSeenTime) {
            agent.stateMachine.ChangeState(agent.initialState);
        }
    }

    public void FixedUpdate(AiAgent agent) {

    }

    public void Exit(AiAgent agent) {
        agent.pathfinder.SetPath(null);
    }
}
