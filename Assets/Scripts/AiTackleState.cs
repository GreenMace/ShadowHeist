using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTackleState : AiState {

    public float jumpTime = 0.3f;
    public float jumpSpeed = 15f;

    public float getUpTime = 0.5f;
    public float windUpTime = 0.15f;

    private float windUpTimeLeft;
    private float jumpTimeLeft;
    private float getUpTimeLeft;

    private Vector2 directionToTarget;
    private float movementSpeed = 0;

    public AiStateId GetId() {
        return AiStateId.Tackle;
    }

    public void Enter(AiAgent agent) {
        agent.pathfinder.canMove = false;
        agent.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        agent.rb.velocity = Vector2.zero;

        windUpTimeLeft = windUpTime;
        jumpTimeLeft = jumpTime;
        getUpTimeLeft = getUpTime;

        directionToTarget = (agent.player.transform.position - agent.transform.position).normalized;

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        agent.transform.rotation = toRotation;
    }

    public void Update(AiAgent agent) {
        
        if (windUpTimeLeft > 0) {
            windUpTimeLeft -= Time.deltaTime;
            return;
        }

        
        if (jumpTimeLeft > 0) {
            if (jumpTimeLeft == jumpTime) {
                agent.spriteSwitcher.SetTransform(1, trans: agent.transform.position, rot: agent.transform.rotation);
                agent.spriteSwitcher.NextSprite();
            }
            jumpTimeLeft -= Time.deltaTime;
            movementSpeed = jumpSpeed;
            return;
        }
        movementSpeed = 0;

        if (getUpTimeLeft > 0) {
            getUpTimeLeft -= Time.deltaTime;
            return;
        }

        agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        
    }

    public void FixedUpdate(AiAgent agent) {
        agent.rb.MovePosition(agent.rb.position + directionToTarget * movementSpeed * Time.fixedDeltaTime);
    }

    public void Exit(AiAgent agent) {
        agent.pathfinder.canMove = true;
        agent.rb.constraints = RigidbodyConstraints2D.None;
        agent.spriteSwitcher.SetTransform(0, trans: agent.transform.position, rot: agent.transform.rotation);
        agent.spriteSwitcher.NextSprite();
    }
}