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
    private CameraTargets currentCameraTarget;
    private float targetCameraSize;
    private int trackedPlayer = 1;
    public const float pixelPerfectDefaultCameraSize = 8.4375f;
    public const float pixelPerfectZoomInCameraSize = 4.21875f;
     public const float pixelPerfectZoomOutCameraSize = 16.875f;
    // Start is called before the first frame update
    void Start()
    {
        targetCameraSize = pixelPerfectDefaultCameraSize;
        if (instance == null){
            instance = this;
        }
        currentCameraTarget = CameraTargets.ActivePlayers;
    }

    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, CalculateCameraTargetPosition(currentCameraTarget), 0.1f );
        myCamera.orthographicSize = Mathf.MoveTowards(myCamera.orthographicSize, targetCameraSize, 0.5f);
    }

    private Vector3 CalculateCameraTargetPosition(CameraTargets cameraTarget){
        if (cameraTarget == CameraTargets.Player) {
            return new Vector3 (
                GameController.instance.playerList[trackedPlayer-1].transform.position.x,
                GameController.instance.playerList[trackedPlayer-1].transform.position.y,
                -10f);
        }
        else if (cameraTarget == CameraTargets.ActivePlayers) {
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
        else  {
         
            return new Vector3 (0,0,-10);
            }

    }
    //lerp in incrmenets
    public void ChangeZoom (float size){
        targetCameraSize = size;
    }
    
    public void ChangeCameraTarget (CameraTargets cameraTarget) {
        this.currentCameraTarget = cameraTarget;
        if (cameraTarget == CameraTargets.ActivePlayers) ChangeZoom(pixelPerfectDefaultCameraSize);
        else if (cameraTarget == CameraTargets.ActivePlayers) ChangeZoom(pixelPerfectZoomInCameraSize);
        else if (cameraTarget == CameraTargets.Stage) ChangeZoom(pixelPerfectZoomOutCameraSize);
    }
    public void ChangePlayerCameraTarget (int playerToTrack) {
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
}
