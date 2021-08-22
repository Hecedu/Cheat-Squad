using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    public ParticleSystem dustJump;
    public ParticleSystem dustLanding;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDustJump(){
		dustJump.Play();
	}
    public void playDustLanding(){
		dustLanding.Play();
	}
}
