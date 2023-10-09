using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HeightScript : MonoBehaviour
{
    PlayerMovement movement;
    ShadowCaster2D shadows;
    // Start is called before the first frame update
    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        shadows = GetComponent<ShadowCaster2D>();
    }

    // Update is called once per frame
    void Update()
    {
        shadows.enabled = movement.crouching;
    }
}
