using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInSightScript : MonoBehaviour
{

    public GameObject player;
    public LayerMask obstructionLayer;

    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
