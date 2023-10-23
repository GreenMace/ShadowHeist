using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideUnderTable : MonoBehaviour
{
    public GameObject playerRef;
    public bool underTable = false;


    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        underTable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        underTable = false;
    }





}
