using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class LifeCounter : MonoBehaviour
{
    private PlayerStats playerStats;
    public string playerName;
    public Image[] Lifes;
    void Start()
    {
        var playerList = GameObject.FindGameObjectsWithTag("Player");
        var player = playerList.First(x=> x.name == playerName);
        
        if(player != null){
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        for (int i  = 0; i < Lifes.Length; i++ ){
            if (i < playerStats.lifeCounter) Lifes[i].enabled = true;
            else Lifes[i].enabled = false;
        }        
    }
}
