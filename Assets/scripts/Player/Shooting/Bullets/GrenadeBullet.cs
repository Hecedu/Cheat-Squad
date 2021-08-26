using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float launchForceX = 300f;
    public float lauchForceY = 400f;
    public float knockbackForceX = 30f;
    public float knockbackForceY = 900f;
    public float time = 2f;
    private bool isColliding = false;
    const float explosionRadius = 1.5f; 
    public LayerMask solidsLayerMask; 

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
            if (other.gameObject.tag == "Player")
            {
                var otherParticleController = other.gameObject.GetComponentInChildren<ParticleController>();
                var otherCharacterController2D = other.gameObject.GetComponent<CharacterController2D>();

                Destroy (gameObject);
                otherParticleController.playParticleEffect("Dust Explosion");
                if (transform.rotation.eulerAngles.y < 180) otherCharacterController2D.StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), true);
                else otherCharacterController2D.StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), false);
 
            }
            else {
                var thisParticleController =transform.GetComponentInChildren<ParticleController>();
                var thisSpriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
                var thisCircleCollider2D = transform.GetComponentInChildren<CircleCollider2D>();
                var isExploding = false;

                thisParticleController.playParticleEffect("Dust Explosion");
                Destroy(thisSpriteRenderer);                
                Destroy(thisCircleCollider2D);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius,solidsLayerMask);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.tag == "Player" && !isExploding)
                    {
                        var colliderCharacterController2D = colliders[i].gameObject.GetComponent<CharacterController2D>();
                        var deltaX = colliders[i].transform.position.x - transform.position.x; 

                        isExploding = true;
                        if (deltaX > 0) colliderCharacterController2D.StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), true);
                        else colliderCharacterController2D.StartKnockback(new Vector2 (knockbackForceX, knockbackForceY), false);
                    }
                }
            }
        }
    }
}
