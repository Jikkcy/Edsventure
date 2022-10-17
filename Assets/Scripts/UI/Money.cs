using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Money : MonoBehaviour
{
    private TextMeshProUGUI tm;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        tm = this.GetComponent<TextMeshProUGUI>();
        tm.text = "0";

        
    }

    // Update is called once per frame
    void Update()
    {
        tm.text = $"{player.money}";
    }
}
