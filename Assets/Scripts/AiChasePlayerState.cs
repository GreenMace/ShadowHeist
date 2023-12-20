using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public Transform playerTransform;
    public float movementSpeed = 3;

    public InfluenceMap searchedMapPlayer;
    public InfluenceMap searchedMap;
    public float searchRange = 4;
    public float searchRange2 = 6;
    public int randomAmong = 1;
    public int randomAmong2 = 3;
    public Vector3 currentDestination;

    float playerLastSeenTime;
    float chaseTime = 5;
    public Propagator propagatorScript;
    public GameObject playerRef;
    

    public AiStateId GetId() {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent) {

        searchedMapPlayer = GameObject.Find("handelInfluenceMapPlayer").GetComponent<InfluenceMap>();
        searchedMap = GameObject.Find("handelInfluenceMap").GetComponent<InfluenceMap>();
        
        if (playerTransform == null) {
            playerTransform = agent.player.transform;
        }

        agent.movementController.maxSpeed = movementSpeed;
    }

    public void Update(AiAgent agent) {
        if (Vector3.Distance(playerTransform.position, agent.transform.position) < 1 && agent.FoV.canSeePlayer) {
            agent.stateMachine.ChangeState(AiStateId.Tackle);
        }
       
        if (agent.FoV.canSeePlayer)
        {
            currentDestination = searchedMapPlayer.getHighestInRangeWorld(agent.transform.position, searchRange2, randomAmong2);
        }
        else if (agent.pathfinder.reachedEndOfPath && !(agent.pathfinder.pathPending))
        {
            currentDestination = searchedMap.getLowestInRangeWorld(agent.transform.position, searchRange, randomAmong);
        }

        agent.pathfinder.destination = currentDestination;
       

        if (agent.FoV.canSeePlayer || agent.SoundDetect.canHearPlayer){
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
