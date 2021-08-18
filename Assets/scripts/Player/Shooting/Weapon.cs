using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Transform crouchFirePoint;
    public GameObject bulletPrefab; 
    public float pushBackForce;
    // Start is called before the first frame update
    public void OnShoot(InputAction.CallbackContext context) {
        if (context.started && !PauseController.gameIsPaused){
            Shoot();
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(){
        SoundManager.instance.PlaySoundEffect($"Shot{UnityEngine.Random.Range(1,4)}",0.2f);
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, new Vector3(firePoint.position.x,firePoint.position.y + 0.2f,firePoint.position.z), firePoint.rotation);
        Instantiate(bulletPrefab, new Vector3(firePoint.position.x,firePoint.position.y - 0.2f,firePoint.position.z), firePoint.rotation);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce((-transform.right)*pushBackForce);
    }
}
