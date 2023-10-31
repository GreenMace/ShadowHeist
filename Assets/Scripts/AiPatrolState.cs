using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrolState : AiState {
    public DestinationCollection destinations;
    public float movementSpeed = 2;

    Destination currentDestination;

    public AiStateId GetId() {
        return AiStateId.Patrol;
    }

    public void Enter(AiAgent agent) {
        destinations = agent.GetComponent<DestinationCollection>();
        currentDestination = destinations.GetDestination();
        agent.movementController.maxSpeed = movementSpeed;
    }

    public void Update(AiAgent agent) {
        bool search = false;

        agent.handleSuspicion();

        if (agent.pathfinder.reachedEndOfPath && !(agent.pathfinder.pathPending)) {
            currentDestination.Act(agent);
        }

        if (currentDestination.finished) {
            currentDestination.Reset();
            currentDestination = destinations.GetDestination(1);
            search = true;
        }

        agent.pathfinder.destination = currentDestination.GetTransform().position;

        if (search) {
            agent.pathfinder.SearchPath();
        }
    }

    public void FixedUpdate(AiAgent agent) {
       
    }


    public void Exit(AiAgent agent) {
        currentDestination.Reset();
        
    }
}