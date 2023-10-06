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
        currentDestination = destinations.GetNextDestination();
        agent.movementController.maxSpeed = movementSpeed;
    }

    public void Update(AiAgent agent) {
        bool search = false;
        
        if (agent.FoV.canSeePlayer || agent.SoundDetect.canHearPlayer) {
            if (!agent.HUT.underTable || GetId() == AiStateId.ChasePlayer )
            {
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }

        if (agent.ai.reachedEndOfPath && !(agent.ai.pathPending)) {
            currentDestination.Act(agent);
        }

        if (currentDestination.finished) {
            currentDestination.Reset();
            currentDestination = destinations.GetNextDestination();    
            search = true;
        }

        agent.ai.destination = currentDestination.GetTransform().position;

        if (search) {
            agent.ai.SearchPath();
        }

    }

    public void Exit(AiAgent agent) {
        currentDestination.Reset();
    }
}