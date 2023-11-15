using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropagator {
    float Value { get; }
}

public class Propagator : MonoBehaviour, IPropagator {
    public float _value;
    public float Value { get { return _value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
