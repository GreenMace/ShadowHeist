using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCollection : MonoBehaviour
{
    public GameObject[] destinations;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Destination GetNextDestination() {
        index += 1;
        index = index % destinations.Length;
        return destinations[index].GetComponent<Destination>();
    }
}
