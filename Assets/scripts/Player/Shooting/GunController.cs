using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using sharedObjects;
public class GunController : MonoBehaviour
{
    public GameObject[] gunList;
    private GameObject equipedGun;
    private Guns lastGunState;
    public Guns currentGunState;
    
    //recoil Variables
    public Transform firePoint;
    // Start is called before the first frame update
    public void OnShoot(InputAction.CallbackContext context) {
        if (context.started && !PauseController.gameIsPaused){
            Shoot();
        }

    }
    void Start()
    {
        currentGunState = Guns.GrenadeGun;
        SwapGun();
  
    }

    // Update is called once per frame
    void Update()
    {
        if (lastGunState != currentGunState) {
            SwapGun();
        }
        lastGunState = currentGunState;
        
    }
    public void Shoot(){
        SoundManager.instance.PlaySoundEffect($"Shot{UnityEngine.Random.Range(1,4)}",0.2f);
        Instantiate(equipedGun.GetComponent<Gun>().bulletPrefab, firePoint.position, firePoint.rotation);
        //Instantiate(bulletPrefab, new Vector3(firePoint.position.x,firePoint.position.y + 0.2f,firePoint.position.z), firePoint.rotation);
        //Instantiate(bulletPrefab, new Vector3(firePoint.position.x,firePoint.position.y - 0.2f,firePoint.position.z), firePoint.rotation);
        
        if (transform.rotation.y > -1) this.gameObject.GetComponentInParent<CharacterController2D>().StartKnockback(new Vector2 (equipedGun.GetComponent<Gun>().recoilForceX, equipedGun.GetComponent<Gun>().recoilForceY), false);
        else this.gameObject.GetComponentInParent<CharacterController2D>().StartKnockback(new Vector2 (equipedGun.GetComponent<Gun>().recoilForceX, equipedGun.GetComponent<Gun>().recoilForceY), true);
    }
    private void SwapGun() {
        foreach (GameObject gun in gunList) {
            if (gun.name != currentGunState.ToString()) gun.SetActive(false);
            else {
                gun.SetActive(true);
                equipedGun = gun;
            }
        }
    }
    public void ChangeEquipedGun(Guns gun) {
        currentGunState = gun;
    }
}
