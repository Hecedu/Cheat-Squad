using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerStats : MonoBehaviour
    {
        public string playerName;
        public int playerNumber;
        public PlayerState playerState;
        public Vector2 spawnPoint;
        public int lifeNumber;
        public float movementSpeed;
        public bool jetPack = false;
        public bool shield = false;
        public bool doubleJump = false;
        public bool doubleShoot = false;
        public string playerInputString = "";
    }
    public enum PlayerState {Respawning, Playing, Defeated};
