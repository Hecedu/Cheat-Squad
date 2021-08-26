using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheatDisplayController : MonoBehaviour
{
    public PlayerStats playerStats;
    public string playerTag;
    public Text playerName;
    public TMP_Text playerCheatDisplay;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindWithTag(playerTag);

        if(player != null) playerStats = player.GetComponent<PlayerStats>();
        playerName.text = playerStats.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        playerCheatDisplay.text = playerStats.playerInputString;
    }
}
