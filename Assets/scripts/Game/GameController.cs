using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sharedObjects;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<PlayerInitData> playerInitDataList;
    public List<GameObject> playerList;
    public int numberOfPlayers;
    public static int maxLifes = 4;
    const float xBound = 25f; 
    const float yBound = 20f;

    void Awake(){
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;

        if (instance == null){
            instance = this;
        }
        if (Gamepad.all.Count() == 0) {
                playerInitDataList = new List<PlayerInitData>(){
                new PlayerInitData("PLAYER 1",1,"KeyboardWASD",new Vector2(-9,-3)),
                new PlayerInitData("PLAYER 2",2,"KeyboardIJKL",new Vector2(9,-3))
            };
        }
        else {
            playerInitDataList = new List<PlayerInitData>(){
                new PlayerInitData("PLAYER 1",1,"KeyboardWASD",new Vector2(-9,-3)),
                new PlayerInitData("PLAYER 2",2,"Gamepad",new Vector2(9,-3))
            };
        }

        InitializeMatch();
    }
    void Start()
    {
        StartCountdown();
    }
    void Update()
    {
        foreach (GameObject player in playerList){
            var currentPlayerStats = player.GetComponent<PlayerStats>();
            if (currentPlayerStats.playerState == PlayerState.Playing) CheckOutOfBounds(player);
            else if (currentPlayerStats.playerState == PlayerState.Defeated) EndMatch(currentPlayerStats);
        }
    }

    private void InitializeMatch(){
        PlayerLoader.instance.InitializePlayers(playerInitDataList, maxLifes);
        
        playerList = GameObject.FindGameObjectsWithTag("Player").ToList(); 
    }
    private void EndMatch(PlayerStats loser){
        foreach (GameObject player in playerList){

            var currentPlayerStats = player.GetComponent<PlayerStats>();
            if (currentPlayerStats.playerState == PlayerState.Playing) {
                CameraController.instance.ChangePlayerCameraTarget(currentPlayerStats.playerNumber);
                CameraController.instance.ChangeCameraTarget(CameraTargets.Player);
                NotificationsController.instance.DisplayResults(currentPlayerStats);
            } else CameraController.instance.ChangeCameraTarget(CameraTargets.Stage);
        }
        StartCoroutine(SlowMotionController.instance.StartMatchEndSlowMotion(5f));
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
        var CharacterController2D = playerToRespawn.transform.GetComponent<CharacterController2D>();
        var gunController = playerToRespawn.GetComponentInChildren<GunController>();

        playerStats.playerState = PlayerState.Respawning;
        playerStats.lifeNumber --;
        SoundManager.instance.PlaySoundEffect($"Death{UnityEngine.Random.Range(1,4)}",0.2f);

        if (playerStats.lifeNumber == 0) playerStats.playerState = PlayerState.Defeated;
        else {
              foreach(GameObject player in playerList){
                var currentPlayerStats = player.GetComponent<PlayerStats>();
                if (currentPlayerStats.playerState != PlayerState.Respawning) allRespawning = false;
            }
            if (allRespawning) CameraController.instance.ChangeCameraTarget(CameraTargets.Stage);
            else CameraController.instance.ChangeCameraTarget(CameraTargets.ActivePlayers);

            yield return new WaitForSeconds(respawnCooldownInSeconds);

            playerStats.playerState = PlayerState.Playing;
            CharacterController2D.ResetMovement();
            gunController.InitializeGunController();
            playerToRespawn.transform.position = playerStats.spawnPoint;
            CameraController.instance.ChangeCameraTarget(CameraTargets.ActivePlayers);
        }
    }
}
