using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSearchState : AiState {
    public float movementSpeed = 2.5f;
    public float searchRange = 5;

    public InfluenceMap searchedMap;
    public Vector3 currentDestination;

    public AiStateId GetId() {
        return AiStateId.Search;
    }

    public void Enter(AiAgent agent) {
        searchedMap = GameObject.Find("handelInfluenceMap").GetComponent<InfluenceMap>();
        agent.movementController.maxSpeed = movementSpeed;

        currentDestination = searchedMap.getLowestInRangeWorld(agent.transform.position, searchRange);
        agent.pathfinder.destination = currentDestination;
        agent.pathfinder.SearchPath();
    }

    public void Update(AiAgent agent) {
        bool search = false;

        agent.handleSuspicion();

        if (agent.pathfinder.reachedEndOfPath && !(agent.pathfinder.pathPending)) {
            search = true;
            currentDestination = searchedMap.getLowestInRangeWorld(agent.transform.position, searchRange);
        }

        agent.pathfinder.destination = currentDestination;

        if (search) {
            agent.pathfinder.SearchPath();
        }
    }

    public void FixedUpdate(AiAgent agent) {

    }


    public void Exit(AiAgent agent) {

    }
}