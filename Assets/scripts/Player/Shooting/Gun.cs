using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sharedObjects;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Guns gunType;
    public int DefaultAmmo;
    public int currentAmmo;
    public float coolDownTime;
    public float recoilForceX;
    public float recoilForceY;
    private void OnEnable() {
        currentAmmo = DefaultAmmo;
    }
    void start() {
    }
    public void DepleteAmmo() {
        if (currentAmmo > 1)currentAmmo --;
        else GetComponentInParent<GunController>().ChangeEquipedGun(Guns.DefaultGun);
    }
}
