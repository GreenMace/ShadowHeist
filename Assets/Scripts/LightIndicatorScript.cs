using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIndicatorScript : MonoBehaviour
{
    public float minScale = 0.5f;
    public float lightScaling = 0.4f;
    private LightCheckScript lightScript;
    private RectTransform lightAmountTransform;

    // Start is called before the first frame update
    void Start()
    {
        lightScript = GameObject.FindGameObjectWithTag("Player").GetComponent<LightCheckScript>();
        lightAmountTransform = transform.Find("Light Amount").gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        lightAmountTransform.localScale = Vector3.one * (minScale + lightScaling * lightScript.LightLevel);
    }
}
