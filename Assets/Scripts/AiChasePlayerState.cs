using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public Transform playerTransform;
    public float movementSpeed = 3;

    public InfluenceMap searchedMap;
    public float searchRange = 5;
    public int randomAmong = 1;

    float playerLastSeenTime;
    float chaseTime = 3;

    public AiStateId GetId() {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent) {

        searchedMap = GameObject.Find("handelInfluenceMapPlayer").GetComponent<InfluenceMap>();
        if (playerTransform == null) {
            playerTransform = agent.player.transform;
        }

        agent.movementController.maxSpeed = movementSpeed;
    }

    public void Update(AiAgent agent) {
        if (Vector3.Distance(playerTransform.position, agent.transform.position) < 1 && agent.FoV.canSeePlayer) {
            agent.stateMachine.ChangeState(AiStateId.Tackle);
        }

        agent.pathfinder.destination = searchedMap.getHighestInRangeWorld(agent.transform.position, searchRange, randomAmong);

        Debug.Log(agent.pathfinder.destination);

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
