using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class AmmoDisplayController : MonoBehaviour
{
    public Text ammoText; 
    public Gun playerGun;
    // Start is called before the first frame update
    
    private void Awake() {
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGun.gunType != sharedObjects.Guns.DefaultGun && playerGun != null) ammoText.text = $"AMMO{playerGun.currentAmmo}";
        else ammoText.text = "";
    }
    public void UpdateGun(Gun currentGun) {
        playerGun = currentGun;
    }
}
