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
            playerInputs.Add(CreatePlayer(players[i],i+1));
        }
    } 
    private PlayerInput CreatePlayer(PlayerInitData player, int playerNumber){
        int[] otherPlayers = GetOtherPlayerLayers(playerNumber);
        PlayerInput currentPlayer = PlayerInput.Instantiate(playerPrefab, controlScheme: player.controlScheme, pairWithDevice: Keyboard.current);
        currentPlayer.tag = $"Player{playerNumber}";
        currentPlayer.gameObject.layer = LayerMask.NameToLayer($"Player{playerNumber}");
        currentPlayer.gameObject.GetComponent<CharacterController2D>().m_WhatIsGround = LayerMask.GetMask("Ground",$"Player{otherPlayers[0]}");
        currentPlayer.gameObject.GetComponent<PlayerStats>().playerName = player.playerName;
        currentPlayer.gameObject.GetComponent<PlayerStats>().spawnPoint = player.spawnPoint;
        currentPlayer.transform.position = player.spawnPoint;
        return currentPlayer;
    }
    private int[] GetOtherPlayerLayers (int playerNumber){
        List<int> playernumbers = new List<int>{1,2};
        playernumbers.Remove(playerNumber);
        return playernumbers.ToArray();
    }
}
