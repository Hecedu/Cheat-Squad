using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem[] particleEffects;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playParticleEffect(string effectName){
        particleEffects.First(x => x.name == effectName).Play();
    }
}
