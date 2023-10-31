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

    public FieldOfView FoV;
    public SoundDetection SoundDetect;
    public Rigidbody2D rb;
    
    public HideUnderTable HUT;

    public GameObject player;
    public SpriteSwitchingScript spriteSwitcher;

    public float suspicionPercent { get; private set; }
    public GameObject suspicionMeterRef;
    public SuspicionMeterScript susScript;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<AIPath>();
        pathfinder = GetComponent<IAstarAI>();
        FoV = GetComponent<FieldOfView>();
        SoundDetect = GetComponent<SoundDetection>();
        player = GameObject.FindGameObjectWithTag("Player");
        HUT = player.GetComponent<HideUnderTable>();
        rb = GetComponent<Rigidbody2D>();
        spriteSwitcher = GetComponent<SpriteSwitchingScript>();

        suspicionMeterRef = transform.Find("Canvas").gameObject.transform.Find("Suspicion Meter").gameObject;
        susScript = suspicionMeterRef.GetComponent<SuspicionMeterScript>();

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiPatrolState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiTackleState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    void FixedUpdate() {
        stateMachine.FixedUpdate();
    }

    public void handleSuspicion() {
        if ((FoV.canSeePlayer || SoundDetect.canHearPlayer) && (!HUT.underTable || stateMachine.currentState == AiStateId.ChasePlayer)) {
            suspicionPercent = Mathf.Min((float)(suspicionPercent + FoV.lightScript.LightLevel * Time.deltaTime), 1);
        } else {
            suspicionPercent = Mathf.Max((float)(suspicionPercent - 0.2 * Time.deltaTime), 0);
        }

        if (suspicionPercent == 1) {
            stateMachine.ChangeState(AiStateId.ChasePlayer);
        }


        if (suspicionPercent > 0) {
            suspicionMeterRef.SetActive(true);
            susScript.suspicionPercent = suspicionPercent;
        } else {
            suspicionMeterRef.SetActive(false);
        }
    }

    public void reset() {
        stateMachine.ChangeState(initialState);
    }
}
