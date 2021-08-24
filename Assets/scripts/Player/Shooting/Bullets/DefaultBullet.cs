using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    public float speed = 20f;
    public float knockbackForceX = 3f;
    public float knockbackForceY = 3f;
    public float time = 2f;
    public Rigidbody2D rb; 
    private bool isColliding = false;

    void Start()
    {
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
            Destroy (gameObject);
            if (other.gameObject.tag == "Player")
            {
                if (transform.rotation.y > -1) other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), true);
                else other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), false);
            
            }
        }
        
    }
}
