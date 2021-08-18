using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using sharedObjects;
using System.Linq;

public class PlayerLoader : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Player> playerList;
    public List<PlayerInput> players;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerList = new List<Player>(){
            new Player("HECEDU","KeyboardWASD",new Vector2(-8,-1)),
            new Player("ABEL","KeyboardIJKL",new Vector2(8,-1))
            };
        players = new List<PlayerInput>();
        for (int i = 0; i<playerList.Count ; i++){
            players.Add(CreatePlayer(playerList[i],i+1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public PlayerInput CreatePlayer(Player player, int playerNumber){
        int[] otherPlayers = GetOtherPlayerLayers(playerNumber);
        PlayerInput currentPlayer = PlayerInput.Instantiate(playerPrefab, controlScheme: player.controlScheme, pairWithDevice: Keyboard.current);
        currentPlayer.tag = $"Player{playerNumber}";
        currentPlayer.gameObject.layer = LayerMask.NameToLayer($"Player{playerNumber}");
        currentPlayer.gameObject.GetComponent<CharacterController2D>().m_WhatIsGround = LayerMask.GetMask("Ground",$"Player{otherPlayers[0]}");
        currentPlayer.gameObject.GetComponent<PlayerStats>().playerName = player.playerName;
        currentPlayer.transform.position = player.spawnPoint;
        return currentPlayer;
    }
    public int[] GetOtherPlayerLayers (int playerNumber){
        List<int> playernumbers = new List<int>{1,2};
        playernumbers.Remove(playerNumber);
        return playernumbers.ToArray();
    }
}
