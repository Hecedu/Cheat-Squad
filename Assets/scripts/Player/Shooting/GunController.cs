using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using sharedObjects;
public class GunController : MonoBehaviour
{
    public GameObject[] gunList;
    public GameObject equipedGun;
    private Guns lastGunState;
    public Guns currentGunState;
    //recoil Variables
    public Transform firePoint;
    //Cooldown Variable
    public float timeStamp;

    public void OnShoot(InputAction.CallbackContext context) {
        if (context.started && !PauseController.gameIsPaused){
            Shoot();
        }

    }

    private void Start() {
    }
    void Update()
    {
        if (lastGunState != currentGunState) {
            SwapGun();
        }
        lastGunState = currentGunState;
    }

    public void Shoot(){
        var equipedGunData = equipedGun.GetComponent<Gun>();
        var characterController2D = transform.GetComponentInParent<CharacterController2D>();
        
        if(timeStamp <= Time.time) {
            timeStamp = Time.time + equipedGunData.coolDownTime;
            if (equipedGunData.gunType != Guns.DefaultGun )equipedGunData.DepleteAmmo();
            SoundManager.instance.PlaySoundEffect($"Shot{UnityEngine.Random.Range(1,4)}",0.2f);
            Instantiate(equipedGunData.bulletPrefab, firePoint.position, firePoint.rotation);
            if (transform.rotation.eulerAngles.y < 180) characterController2D.StartKnockback(new Vector2 (equipedGunData.recoilForceX, equipedGunData.recoilForceY), false);
            else characterController2D.StartKnockback(new Vector2 (equipedGunData.recoilForceX, equipedGunData.recoilForceY), true);
        } 
    }
    private void SwapGun() {

        foreach (GameObject gunGameObject in gunList) {
            var gun = gunGameObject.GetComponent<Gun>();
            if (gun.gunType == currentGunState) {
                gunGameObject.SetActive(true);
                equipedGun = gunGameObject;
                transform.GetComponentInParent<CharacterDisplayController>().UpdateAmmoDisplay(gun);
            }
            else{  
                gunGameObject.SetActive(false);
            }
        }
    }
    public void ChangeEquipedGun(Guns gun) {
        currentGunState = gun;
    }
    public void InitializeGunController(){
        currentGunState = Guns.DefaultGun;
        timeStamp = Time.time;
        SwapGun();
    }
}
