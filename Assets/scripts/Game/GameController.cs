using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sharedObjects;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<PlayerInitData> playerInitDataList;
    public List<GameObject> playerList;
    float xBound = 15f; 
    float yBound = 10f;
    // Start is called before the first frame update
    void Awake(){
        //set static instance
        if (instance == null){
            instance = this;
        }
        //retrive/create init data list
        playerInitDataList = new List<PlayerInitData>(){
            new PlayerInitData("HECEDU","KeyboardWASD",new Vector2(-8,2)),
            new PlayerInitData("ABEL","KeyboardIJKL",new Vector2(8,2))
        };
       
    }
    void Start()
    {
         //spawn players 
        PlayerLoader.instance.InitializePlayers(playerInitDataList);
        //Set player list for checks
        playerList = new List<GameObject>{GameObject.FindWithTag("Player1"),GameObject.FindWithTag("Player2")};
        BeginCountDown();

        
        //Disable Input
        //SpawnPlayers
        //Display countdown 
        //Enable Input
    }

    void Update()
    {
        CheckOutOfBounds(playerList);
    }
    void BeginCountDown(){

    }
    void StartMatch(){

    }
    void PauseGame(){

    }
    void EndMatch(){

    }
    public void CheckOutOfBounds(List<GameObject> playerList){
        foreach (GameObject player in playerList){
            if (player.transform.position.x > xBound || player.transform.position.x < -xBound|| player.transform.position.y > yBound || player.transform.position.y < -yBound) {
               

            }
        }
    }
    public void RespawnPlayer (GameObject player) {
        StartCoroutine(CameraController.instance.Shake(0.5f,0.1f)); 
        SoundManager.instance.PlaySoundEffect($"Death{UnityEngine.Random.Range(1,4)}",0.2f);
        var playerStats = player.GetComponent<PlayerStats>();
        playerStats.lifeCounter --;
        player.transform.position = playerStats.spawnPoint;
    }
}
