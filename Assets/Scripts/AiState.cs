using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId {
    ChasePlayer,
    Patrol,
    Idle,
    Tackle
}

public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void FixedUpdate(AiAgent agent);
    void Exit(AiAgent agent);
}
