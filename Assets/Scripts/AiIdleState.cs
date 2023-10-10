using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState {

    public AiStateId GetId() {
        return AiStateId.Idle;
    }

    public void Enter(AiAgent agent) {
    }

    public void Update(AiAgent agent) {
        agent.handleSuspicion();
    }

    public void Exit(AiAgent agent) {

    }
}