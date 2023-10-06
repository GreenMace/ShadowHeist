using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public bool finished = false;
    public float waitTime = 1;

    float waitUntil = float.PositiveInfinity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Act(AiAgent agent) {
        if (float.IsPositiveInfinity(waitUntil)) {
            waitUntil = Time.time + waitTime;
            //Debug.Log(Time.time);
            //Debug.Log(waitUntil);
            //Debug.Log(GetTransform().position);
        }

        if (Time.time >= waitUntil) {
            finished = true;
        }
    }
    
    public Transform GetTransform() {
        return gameObject.transform;
    }

    public void Reset() {
        waitUntil = float.PositiveInfinity;
        finished = false;
    }
}
