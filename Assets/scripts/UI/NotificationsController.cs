using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsController : MonoBehaviour
{
    public static NotificationsController instance;
    public Text notificationsText;
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        notificationsText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayResults(PlayerStats winnerStats){
        notificationsText.text = $"OPPONENT DESTROYED \n \n {winnerStats.playerName} WINS!!";
    }
    public void DisplayCheatNotification(){
        
    }
}
