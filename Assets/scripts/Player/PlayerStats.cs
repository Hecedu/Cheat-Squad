using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerName;
    public int lives;
    public float movementSpeed;
    public bool jetPack;
    public bool shield;
    public bool doubleJump;
    public bool doubleShoot;
    public string playerInputString;

    // Start is called before the first frame update
    void Awake()
    {
        lives = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
