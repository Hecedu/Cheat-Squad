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
    public enum CameraTargets {
        Player,
        Stage,
        ActivePlayers
        };
}