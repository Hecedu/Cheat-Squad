using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBullet : MonoBehaviour
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
    }

    void Update()
    {
        isColliding = false;   
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (!isColliding) {
            isColliding = true;
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(SlowMotionController.instance.StartHitStun(0.5f, this.gameObject));
                if (transform.rotation.eulerAngles.y < 180) other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), true);
                else other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), false);
            
            }
        }
        
    }
}
