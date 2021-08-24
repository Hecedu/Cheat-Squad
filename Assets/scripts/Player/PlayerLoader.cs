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
    public List<PlayerInput> playerInputs;
    
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
    public void InitializePlayers (List<PlayerInitData> players){
        playerInputs = new List<PlayerInput>();
        for (int i = 0; i<players.Count ; i++){
            playerInputs.Add(CreatePlayer(players[i]));
        }
    } 
    private PlayerInput CreatePlayer(PlayerInitData playerInitData){
        PlayerInput currentPlayer = PlayerInput.Instantiate(playerPrefab, controlScheme: playerInitData.controlScheme, pairWithDevice: Keyboard.current);
        InitializePlayerStats(playerInitData, currentPlayer);
        SetTagAndLayers(currentPlayer);
        if (playerInitData.spawnPoint.x > 0) currentPlayer.transform.rotation = Quaternion.Euler(0, -180, 0);
        currentPlayer.transform.position = playerInitData.spawnPoint;
        return currentPlayer;
    }
    private void SetTagAndLayers (PlayerInput player) {
        var playerNumber = player.gameObject.GetComponent<PlayerStats>().playerNumber;
        player.name = $"Player{playerNumber}";
        player.tag = "Player";
        player.gameObject.layer = LayerMask.NameToLayer($"Solids");
        player.gameObject.GetComponent<CharacterController2D>().solidsLayerMask = LayerMask.GetMask("Solids");
    }
    private void InitializePlayerStats (PlayerInitData playerInitData, PlayerInput currentPlayer) {
        var currentPlayerStats = currentPlayer.gameObject.GetComponent<PlayerStats>();
        currentPlayerStats.playerName = playerInitData.playerName;
        currentPlayerStats.spawnPoint = playerInitData.spawnPoint;
        currentPlayerStats.playerState = PlayerState.Playing;
        currentPlayerStats.playerNumber = playerInitData.playerNumber;
    }
}

