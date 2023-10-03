using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundDetection : MonoBehaviour
{
    public float radius = 1;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;
    public bool canHearPlayer { get; private set; }

    [Header("Gizmo")]
    public Color gizmoColor = Color.green;
    public bool showGizmos = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(SoundCheck());
    }
    
    private IEnumerator SoundCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Sound();
        }
    }
    
    private void Sound()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius , targetLayer);
        
        if (canHearPlayer)
        {
            canHearPlayer = false;
        }

        if (rangeCheck.Length <= 0)
        {
            return;
        }

        Transform target = rangeCheck[0].transform;

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
        {
            canHearPlayer = true;
            Gizmos.color = Color.red;

        }
    }
    
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoColor;
            if (canHearPlayer)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(transform.position, radius);
        }
    }


}
