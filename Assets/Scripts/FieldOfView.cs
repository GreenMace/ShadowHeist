using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5;
    [Range(1, 360)] public float angle = 45;
    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    public GameObject playerRef;
    public LightCheckScript lightScript;
    public PlayerMovement movementScript;
    public bool canSeePlayer { get; private set; }

    // Start is called before the first frame update
    void Start() {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        lightScript = playerRef.GetComponent<LightCheckScript>();
        movementScript = playerRef.GetComponent<PlayerMovement>();
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true) {
            yield return wait;
            FOV();
        }
    }

    private void FOV() {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius * lightScript.LightLevel, targetLayer);

        if (canSeePlayer) {
            canSeePlayer = false;
        }

        if(rangeCheck.Length <= 0 ) {
            return;
        }

        Transform target = rangeCheck[0].transform;
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        if (Vector2.Angle(transform.up, directionToTarget) >= angle / 2) {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        LayerMask obstructionLayer = obstacleLayer;
        if (movementScript.crouching) {
            obstructionLayer |= (1 << LayerMask.NameToLayer("Half-Obstacle"));
        }

        if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer)) {
            canSeePlayer = true;
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    
        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -angle/2);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);

        if (canSeePlayer) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }
    
    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
}
