using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using sharedObjects;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Camera myCamera;
    private cameraTargets currentCameraTarget;
    public PixelPerfectCamera cameraLOL;
    private float targetCameraSize;
    private int trackedPlayer = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        targetCameraSize = 8.4375f;
        if (instance == null){
            instance = this;
        }
        currentCameraTarget = cameraTargets.ActivePlayers;
    }

    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, CalculateCameraTargetPosition(currentCameraTarget), 0.1f );
        myCamera.orthographicSize = Mathf.MoveTowards(myCamera.orthographicSize, targetCameraSize, 0.5f);
    }

    private Vector3 CalculateCameraTargetPosition(cameraTargets cameraTarget){
        if (cameraTarget == cameraTargets.Player) return new Vector3 (
                GameController.instance.playerList[trackedPlayer-1].transform.position.x,
                GameController.instance.playerList[trackedPlayer-1].transform.position.y,
                -10f);
        else if (cameraTarget == cameraTargets.ActivePlayers) {
            var playerList = GameController.instance.playerList;
            Vector3 averageCameraPosition = new Vector3(0f,0f,-10f);
            int activePlayercount = 0;
            foreach (GameObject player in  playerList){
                 if (player.GetComponent<PlayerStats>().playerState == PlayerState.Playing) {
                     activePlayercount ++;
                     averageCameraPosition.x += player.transform.position.x;
                     averageCameraPosition.y += player.transform.position.y;
                }
            }
            averageCameraPosition.x = averageCameraPosition.x/activePlayercount;
            averageCameraPosition.y = averageCameraPosition.y/activePlayercount;
            return averageCameraPosition;
        }
                
        else return new Vector3 (0,0,-10);
    }
    //lerp in incrmenets
    public void ChangeZoom (float size){
        targetCameraSize = size;
    }
    
    public void ChangeCameraTarget (cameraTargets cameraTarget) {
        this.currentCameraTarget = cameraTarget;
    }
    public void ChangeCameraTarget (cameraTargets cameraTarget, int playerToTrack) {
        this.currentCameraTarget = cameraTarget;
        this.trackedPlayer = playerToTrack;
    }
    public IEnumerator Shake (float duration, float magnitude) {
        float elapsed = 0.0f;

        while (elapsed < duration){
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;
            this.transform.position += new Vector3 (x,y,0);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    private float GetLargestNumber (float num1, float num2) {
        if (num1> num2) return num1;
        else return num2;
    }

}
