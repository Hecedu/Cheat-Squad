using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class LifeCounterController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image[] Lifes;
    void Start()
    {
    }

    void Update()
    {
       for (int i  = 0; i < Lifes.Length; i++ ){
            if (i < playerStats.lifeNumber) Lifes[i].enabled = true;
            else Lifes[i].enabled = false;
        }  
      
    }
    public void InitializeLifeCounter (PlayerStats myPlayerStats) {
        playerStats = myPlayerStats;
    }
}
