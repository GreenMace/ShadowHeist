using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropagator {
    float Value { get; }
}

public class Propagator : MonoBehaviour, IPropagator {
    public float _value;
    public float Value { get { return _value; } }
    public InfluenceMap infmap;
    bool hasadded = false;
    public string MapName;

    // Start is called before the first frame update
    void Start()
    {
        infmap = GameObject.Find(MapName).GetComponent<InfluenceMap>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasadded)
        {
            infmap.RegisterPropagator(this);
            hasadded = true;
        }
        
    }
}
