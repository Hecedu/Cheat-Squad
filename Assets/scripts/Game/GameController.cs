using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sharedObjects;
using UnityEngine.InputSystem;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<PlayerInitData> playerInitDataList;
    public List<GameObject> playerList;
    public int numberOfPlayers;
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
            new PlayerInitData("HECEDU",1,"KeyboardWASD",new Vector2(-4,2)),
            new PlayerInitData("ABEL",2,"KeyboardIJKL",new Vector2(4,2))
        };
       
    }
    void Start()
    {
         //spawn players 
        PlayerLoader.instance.InitializePlayers(playerInitDataList);
        //Set player list for checks
        playerList = new List<GameObject>{GameObject.FindWithTag("Player1"),GameObject.FindWithTag("Player2")};
        //Display countdown 
        //Enable Input
    }

    void Update()
    {
        foreach (GameObject player in playerList){
            var currentPlayerState = player.GetComponent<PlayerStats>().playerState;
            if (currentPlayerState == PlayerState.Playing) CheckOutOfBounds(player);
            else if (currentPlayerState == PlayerState.Defeated) EndMatch();
        }
    }
    void BeginCountDown(){

    }
    void StartMatch(){

    }
    void EndMatch(){

    }
    public void CheckOutOfBounds(GameObject player){
        if (player.transform.position.x > xBound || player.transform.position.x < -xBound|| player.transform.position.y > yBound || player.transform.position.y < -yBound) {
            StartCoroutine(CameraController.instance.Shake(0.5f,0.5f)); 
            StartCoroutine(RespawnPlayer(player, 4f));
        }
    }
    public int[] GetOtherPlayerNumbers (int myPlayerNumber) {
        return playerInitDataList.Select(playerData => playerData.playerNumber).Where(x => x != myPlayerNumber).ToArray();
    }
    IEnumerator RespawnPlayer (GameObject playerToRespawn, float respawnCooldownInSeconds) {
        var allRespawning = true;
        var playerStats = playerToRespawn.GetComponent<PlayerStats>();

        
        playerStats.playerState = PlayerState.Respawning;
        playerStats.lifeCounter --;
        SoundManager.instance.PlaySoundEffect($"Death{UnityEngine.Random.Range(1,4)}",0.2f);
        foreach(GameObject player in playerList){
            if (player.GetComponent<PlayerStats>().playerState != PlayerState.Respawning) allRespawning = false;
        }

        if (allRespawning) CameraController.instance.ChangeCameraTarget(cameraTargets.Stage);
        else CameraController.instance.ChangeCameraTarget(cameraTargets.ActivePlayers);
        

        yield return new WaitForSeconds(respawnCooldownInSeconds);
        CameraController.instance.ChangeCameraTarget(cameraTargets.ActivePlayers);
        playerToRespawn.transform.position = playerStats.spawnPoint;
        playerStats.playerState = PlayerState.Playing;
    }
}
