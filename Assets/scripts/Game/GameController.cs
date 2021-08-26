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
    public static int maxLifes = 4;
    const float xBound = 15f; 
    const float yBound = 10f;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        playerInitDataList = new List<PlayerInitData>(){
            new PlayerInitData("HECEDU",1,"KeyboardWASD",new Vector2(-7,2)),
            new PlayerInitData("ABEL",2,"KeyboardIJKL",new Vector2(7,2))
        };
        InitializeMatch();
    }
    void Start()
    {
        StartCountdown();
    }
    void Update()
    {
        foreach (GameObject player in playerList){
            var currentPlayerState = player.GetComponent<PlayerStats>().playerState;
            if (currentPlayerState == PlayerState.Playing) CheckOutOfBounds(player);
            else if (currentPlayerState == PlayerState.Defeated) EndMatch();
        }
    }

    private void InitializeMatch(){
        
        PlayerLoader.instance.InitializePlayers(playerInitDataList);
        
        playerList = GameObject.FindGameObjectsWithTag("Player").ToList(); 
    }
    private void EndMatch(){

    }

    private void StartCountdown(){
        if (PauseController.instance != null ){ 
            PauseController.instance.Resume(false);
             StartCoroutine(CountdownController.instance.StartCountdown(3));
        }
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
            if (playerStats.playerState != PlayerState.Respawning) allRespawning = false;
        }
        if (allRespawning) CameraController.instance.ChangeCameraTarget(CameraTargets.Stage);
        else CameraController.instance.ChangeCameraTarget(CameraTargets.ActivePlayers);

        yield return new WaitForSeconds(respawnCooldownInSeconds);

        playerStats.playerState = PlayerState.Playing;
        playerToRespawn.transform.GetComponent<CharacterController2D>().ResetMovement();
        playerToRespawn.transform.position = playerStats.spawnPoint;
        CameraController.instance.ChangeCameraTarget(CameraTargets.ActivePlayers);
    }
}
