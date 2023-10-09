using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionMeterScript : MonoBehaviour
{
    public GameObject parent;
    public Image suspicionFillRef;
    public Vector3 iconOffset;
    public float suspicionPercent;

    // Start is called before the first frame update
    void Start()
    {
        suspicionFillRef = transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = parent.transform.position + iconOffset;
        suspicionFillRef.fillAmount = suspicionPercent;
    }
}
