using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float force = 3f;
    public float time = 2f;
    public Rigidbody2D rb; 

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy (gameObject, time);
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" )
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right*force);
        }
    }
}
