using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sharedObjects {

    public class PlayerInitData : MonoBehaviour {
        public string playerName;
        public string controlScheme;
        public Vector2 spawnPoint;

        public PlayerInitData(string _playerName, string _controlScheme, Vector2 _spawnPoint){
            controlScheme = _controlScheme;
            playerName = _playerName;
            spawnPoint = _spawnPoint;
        }
    }

    public class Cheat {
        public CheatInputs[] cheatCode;
        public Perks perk;

        public Cheat() {
            cheatCode = new CheatInputs[0];
            perk = Perks.DoubleJump;
        }
        public Cheat(int cheatLength){
             cheatCode = new CheatInputs[cheatLength];
            perk = Perks.DoubleJump;
        }

    }
    public enum CheatInputs
    {
        Up,
        Down,
        Left,
        Right,
        Jump,
        Shoot,
        Start
    }
    public enum Perks {
        ExtraLife,
        RemoveEnemyLife,
        InstantWin,
        SpeedBoost,
        DoubleJump,
        JetPack,
        Shield,
    }

    public enum GameStates {
        Countdown,
        Game,
        Results
    }
    public enum cameraTargets {
        Player1,
        Player2,
        Both
        };
}