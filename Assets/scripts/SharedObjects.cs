using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sharedObjects {

    public class PlayerInitData{
        public string playerName;
        public int playerNumber;
        public string controlScheme;
        public Vector2 spawnPoint;

        public PlayerInitData(string _playerName, int _playerNumber, string _controlScheme, Vector2 _spawnPoint){
            controlScheme = _controlScheme;
            playerNumber = _playerNumber;
            playerName = _playerName;
            spawnPoint = _spawnPoint;
        }
    }
    public class GunCheat : Cheat {
        public Guns gun;

        public GunCheat(){
            cheatCode = new CheatInputs[0];
        }
        
        public GunCheat(int cheatLength){
             cheatCode = new CheatInputs[cheatLength];
        }
    }
    public class Cheat {
        public CheatInputs[] cheatCode;

        public Cheat() {
            cheatCode = new CheatInputs[0];
        }
        public Cheat(int cheatLength){
             cheatCode = new CheatInputs[cheatLength];
        }

    }
    public enum CheatInputs{
        Up,
        Down,
        Left,
        Right,
        Jump,
        Shoot
    }

    public enum Guns {
        DefaultGun,
        TripleGun,
        ShotGun,
        GrenadeGun,
        MiniGun,
    } 
    public enum GameStates {
        Countdown,
        Game,
        Results
    }
    public enum CameraTargets {
        Player,
        Stage,
        ActivePlayers
    }
}