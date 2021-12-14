using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplayController : MonoBehaviour
{
    public CharacterDisplay characterDisplay;
    public Text floatingPlayerName;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        floatingPlayerName.transform.rotation = Quaternion.identity;
    }
    public void linkCharacterDisplay(){
        var playerId = this.gameObject.name;
        var characterDisplayList = GameObject.FindGameObjectsWithTag("PlayerDisplay");
        var playerStats = transform.GetComponent<PlayerStats>();
        foreach (GameObject currentDisplay in characterDisplayList){
            var thisCharacterDisplay =  currentDisplay.GetComponent<CharacterDisplay>();
            if (thisCharacterDisplay.playerId == playerId) {
                characterDisplay = thisCharacterDisplay;
                characterDisplay.InitializeCharacterDisplay(this.gameObject);
            }
        }

        floatingPlayerName.text = playerStats.playerName;
    }
    public void UpdateAmmoDisplay (Gun currentGun) {
        characterDisplay.UpdateAmmoDisplay(currentGun);
    }
}
