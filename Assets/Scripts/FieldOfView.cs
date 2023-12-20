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
    public Propagator propagatorScript;
    public bool canSeePlayer { get; private set; }


    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private float startingAngle;
    private Vector3 origin;
    private GameObject fieldOfViewIndicator;
    // Start is called before the first frame update
    void Start() {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        lightScript = playerRef.GetComponent<LightCheckScript>();
        movementScript = playerRef.GetComponent<PlayerMovement>();
        propagatorScript = playerRef.GetComponent<Propagator>();

        mesh = new Mesh();
        fieldOfViewIndicator = transform.Find("Field of View").gameObject;
        fieldOfViewIndicator.GetComponent<MeshFilter>().mesh = mesh;
        meshRenderer = fieldOfViewIndicator.GetComponent<MeshRenderer>();

        StartCoroutine(FOVCheck());
    }

    private void LateUpdate () {
        origin = transform.position;
        int rayCount = 100;
        float currentAngle = GetAngleFromVectorFloat(transform.up) + angle /2f;
        float angleIncrease = angle / rayCount;


        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), radius, obstacleLayer);

            if (raycastHit2D.collider == null) {
                vertex = origin + GetVectorFromAngle(currentAngle) * radius;
            } else {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;


            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            currentAngle -= angleIncrease;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();

        Vector2 directionToTarget = (playerRef.transform.position - transform.position).normalized;

        float distanceToTarget = Vector2.Distance(transform.position, playerRef.transform.position);
        if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer)) {
            meshRenderer.enabled = true;
        } else {
            meshRenderer.enabled = false;
        }

        fieldOfViewIndicator.transform.rotation = Quaternion.identity;
        fieldOfViewIndicator.transform.position = Vector3.zero;
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
            propagatorScript._value = 0;
        }

        if(rangeCheck.Length <= 0 ) {
            return;
        }

        Transform target = rangeCheck[0].transform;
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        if (Vector2.Angle(transform.up, directionToTarget) >= angle / 2f) {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        LayerMask obstructionLayer = obstacleLayer;
        if (movementScript.crouching) {
            obstructionLayer |= (1 << LayerMask.NameToLayer("Half-Obstacle"));
        }

        if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer)) {
            canSeePlayer = true;
            propagatorScript._value = 1;
        }
        
    }
    /*
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
    */
    private Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0 ) {
            n += 360;
        }

        return n;
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees) {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
  
}
