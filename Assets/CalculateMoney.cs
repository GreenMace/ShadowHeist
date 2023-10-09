using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateMoney : MonoBehaviour
{
    public GameObject playerRef;
    public int moneyCount = 0;
    public TextMeshProUGUI moneyText;


    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Loot") == true)
        {
            moneyCount += 10;
            moneyText.text = "Money: " + moneyCount.ToString();
            Destroy(collision.gameObject);
        }
    }
    
}
