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
        currentPlayer.transform.position = playerInitData.spawnPoint;
        return currentPlayer;
    }
    private void SetTagAndLayers (PlayerInput player) {
        var playerNumber = player.gameObject.GetComponent<PlayerStats>().playerNumber;
        int[] otherPlayers = GetOtherPlayerLayers(playerNumber);
        player.tag = $"Player{playerNumber}";
        player.gameObject.layer = LayerMask.NameToLayer($"Player{playerNumber}");
        player.gameObject.GetComponent<CharacterController2D>().groundLayerMask = LayerMask.GetMask("Ground",$"Player{otherPlayers[0]}");
    }
    private void InitializePlayerStats (PlayerInitData playerInitData, PlayerInput currentPlayer) {
        var currentPlayerStats = currentPlayer.gameObject.GetComponent<PlayerStats>();
        currentPlayerStats.playerName = playerInitData.playerName;
        currentPlayerStats.spawnPoint = playerInitData.spawnPoint;
        currentPlayerStats.playerState = PlayerState.Playing;
        currentPlayerStats.playerNumber = playerInitData.playerNumber;
    }
        private int[] GetOtherPlayerLayers (int playerNumber){
        List<int> playernumbers = new List<int>{1,2};
        playernumbers.Remove(playerNumber);
        return playernumbers.ToArray();
    }
}
