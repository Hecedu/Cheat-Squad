using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using sharedObjects;
using System.Linq;

public class PlayerLoader : MonoBehaviour
{
    public static PlayerLoader instance;
    public GameObject playerPrefab;
    private List<PlayerInput> playerInputInstances;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializePlayers (List<PlayerInitData> players, int maxLifes){
        playerInputInstances = new List<PlayerInput>();
        int gamePadCount = 0;
        for (int i = 0; i<players.Count ; i++){
            
            playerInputInstances.Add(CreatePlayer(players[i], maxLifes, ref gamePadCount));
        }
    } 
    private PlayerInput CreatePlayer(PlayerInitData playerInitData, int maxLifes, ref int gamePadCount){
        var currentPlayer = new PlayerInput();
        if(playerInitData.controlScheme.Contains("Keyboard")) {
            currentPlayer = PlayerInput.Instantiate(playerPrefab, controlScheme: playerInitData.controlScheme, pairWithDevice: Keyboard.current);
        }
        else {
            currentPlayer = PlayerInput.Instantiate(playerPrefab, controlScheme: playerInitData.controlScheme, pairWithDevice: Gamepad.all[gamePadCount]);
            gamePadCount++;
        }
        var characterDisplayController = currentPlayer.GetComponent<CharacterDisplayController>();
        var gunController = currentPlayer.GetComponentInChildren<GunController>();

        InitializePlayerStats(playerInitData, currentPlayer, maxLifes);
        SetIdentifiers(currentPlayer);
        characterDisplayController.linkCharacterDisplay();
        gunController.InitializeGunController();
        if (playerInitData.spawnPoint.x > 0) currentPlayer.transform.rotation = Quaternion.Euler(0, -180, 0);
        currentPlayer.transform.position = playerInitData.spawnPoint;
        return currentPlayer;
    }
    private void SetIdentifiers (PlayerInput player) {
        var playerNumber = player.gameObject.GetComponent<PlayerStats>().playerNumber;
        var characterController2D =  player.gameObject.GetComponent<CharacterController2D>();
        
        player.name = $"Player{playerNumber}";
        player.tag = "Player";
        player.gameObject.layer = LayerMask.NameToLayer($"Solids");
        characterController2D.solidsLayerMask = LayerMask.GetMask("Solids");
    }
    private void InitializePlayerStats (PlayerInitData playerInitData, PlayerInput currentPlayer, int maxLifes) {
        var currentPlayerStats = currentPlayer.gameObject.GetComponent<PlayerStats>();
        
        currentPlayerStats.playerName = playerInitData.playerName;
        currentPlayerStats.playerNumber = playerInitData.playerNumber;
        currentPlayerStats.playerState = PlayerState.Playing;
        currentPlayerStats.lifeNumber = maxLifes;
        currentPlayerStats.spawnPoint = playerInitData.spawnPoint;
    }
}

