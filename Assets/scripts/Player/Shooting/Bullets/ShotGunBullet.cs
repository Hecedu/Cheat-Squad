using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : MonoBehaviour
{
    public float speed = 20f;
    public float knockbackForceX = 3f;
    public float knockbackForceY = 3f;
    private float time = 1f;
    public float angle = 0f;
    public Rigidbody2D rb; 
    private bool isColliding = false;

    void Start()
    {
        transform.rotation = Quaternion.Euler(  transform.rotation.eulerAngles.x,
                                                transform.rotation.eulerAngles.y,
                                                transform.rotation.eulerAngles.z + angle);
        rb.velocity = transform.right * speed;
        Destroy (gameObject, time);
    }

    void Update()
    {
        isColliding = false;   
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (!isColliding) {
            isColliding = true;
            if (other.gameObject.layer == LayerMask.NameToLayer("Solids")) Destroy (gameObject);
            if (other.gameObject.tag == "Player")
            {
                
                if (transform.rotation.eulerAngles.y < 180) other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), true);
                else other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), false);
            
            }
            
        }
        
    }
}
