using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float launchForceX = 300f;
    public float lauchForceY = 400f;
    public float knockbackForceX = 100f;
    public float knockbackForceY = 900f;
    public float time = 2f;
    private bool isColliding = false;
    const float explosionRadius = 10f; 

    void Start()
    {
        //rb.velocity = transform.right * speed;
        rb.AddForce(new Vector2(transform.right.x * launchForceX, lauchForceY));
        Destroy (gameObject, time);
    }

    void Update()
    {
        isColliding = false;
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
         if (!isColliding) {
            isColliding = true;
            Destroy (gameObject);

            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponentInChildren<ParticleController>().playParticleEffect("Dust Explosion");
                if (transform.rotation.y > -1) other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), true);
                else other.gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), false);
 
            }
            else {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.NameToLayer("Solids"));
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.tag == "Player")
                    {
                        if (colliders[i].transform.rotation.y > -1) colliders[i].gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), true);
                        else colliders[i].gameObject.GetComponent<CharacterController2D>().StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), false);
                    }
                }
            }
        }
    }
}
