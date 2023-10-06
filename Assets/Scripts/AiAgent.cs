using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;

    public AIPath movementController;
    public IAstarAI pathfinder;
    public IAstarAI ai;

    public FieldOfView FoV;
    public SoundDetection SoundDetect;
    public HideUnderTable HUT;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<AIPath>();
        pathfinder = GetComponent<IAstarAI>();
        ai = GetComponent<IAstarAI>();
        FoV = GetComponent<FieldOfView>();
        SoundDetect = GetComponent<SoundDetection>();
        player = GameObject.FindGameObjectWithTag("Player");
        HUT = player.GetComponent<HideUnderTable>();


        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiPatrolState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
