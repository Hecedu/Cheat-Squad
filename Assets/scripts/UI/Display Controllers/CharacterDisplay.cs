using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public string playerId;
    public GameObject player;
    private Text playerNameDisplay;
    public AmmoDisplayController ammoDisplayController;
    public LifeCounterController lifeCounterController;


    void Awake() {
        ammoDisplayController = transform.GetComponentInChildren<AmmoDisplayController>();
        lifeCounterController = transform.GetComponentInChildren<LifeCounterController>();
        playerNameDisplay = transform.GetComponentInChildren<Text>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeCharacterDisplay (GameObject myPlayer) {
        player = myPlayer;
        var playerStats  = player.GetComponent<PlayerStats>();
        SetPlayerName(playerStats.playerName);
        initializeLifeCounter(playerStats);
    }
    public void SetPlayerName (string playerName) {
        playerNameDisplay.text = playerName;
    }
    public void UpdateAmmoDisplay (Gun currentGun) {
        ammoDisplayController.UpdateGun(currentGun);
    }
    public void initializeLifeCounter (PlayerStats playerStats) {
        lifeCounterController.InitializeLifeCounter(playerStats);
    }
}
