using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeEffectApplier : MonoBehaviour
{
    public static CheatCodeEffectApplier instance;
    // Start is called before the first frame update
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
