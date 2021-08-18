using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    private PlayerStats playerStats;
    public string playerTag;
    public Image[] Lifes;
    void Start()
    {
        var player = GameObject.FindWithTag(playerTag);
        if(player != null){
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        for (int i  = 0; i < Lifes.Length; i++ ){
            if (i < playerStats.lives) Lifes[i].enabled = true;
            else Lifes[i].enabled = false;
        }        
    }
}
