using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class AmmoDisplayController : MonoBehaviour
{
    public static AmmoDisplayController instance;
    public Text ammoText; 
    public string playerName;
    private Gun playerGun;
    // Start is called before the first frame update
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        UpdateGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGun.gunType != sharedObjects.Guns.DefaultGun) ammoText.text = $"AMMO{playerGun.currentAmmo}";
        else ammoText.text = "";
    }
    public void UpdateGun() {
        var playerList = GameObject.FindGameObjectsWithTag("Player");
        var player = playerList.First(x=> x.name == playerName);

        if(player != null) playerGun = player.GetComponentInChildren<GunController>().equipedGun.GetComponent<Gun>();
    }
}
