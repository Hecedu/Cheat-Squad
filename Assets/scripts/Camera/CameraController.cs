using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using sharedObjects;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Transform player1;
    private Transform player2;
    private Vector3 target;
    public Vector3 effectsOffset;
    public PixelPerfectCamera pixelPerfectCamera;
    public cameraTargets currentCameraTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null){
            instance = this;
        }
        player1 = GameObject.FindWithTag("Player1").transform;
        player2 = GameObject.FindWithTag("Player2").transform;
        ZoomOut();
        currentCameraTarget = cameraTargets.Both;
    }

    void Update()
    {
      this.transform.position = CalculateCameraTargetPosition(currentCameraTarget)+effectsOffset;
    }

    private Vector3 CalculateCameraTargetPosition(cameraTargets cameraTarget){
        switch (cameraTarget){
            case cameraTargets.Player1: return new Vector3 (
                player1.transform.position.x,
                player1.transform.position.y,
                -10); 
            case cameraTargets.Player2: return new Vector3 (
                player2.transform.position.x,
                player2.transform.position.y,
                -10); 
            default: return new Vector3 (
                (player1.transform.position.x + player2.transform.position.x)/2,
                GetLargestNumber(player1.position.y, player2.position.y),
                -10); 
        }
    }
    public void ZoomIn (){
        pixelPerfectCamera.refResolutionX = 320;
        pixelPerfectCamera.refResolutionY = 180;
    }
    public void ZoomOut (){
        pixelPerfectCamera.refResolutionX = 640;
        pixelPerfectCamera.refResolutionY = 360;
    }
    public void ChangeCameraTarget (cameraTargets cameraTarget) {
        this.currentCameraTarget = cameraTarget;
    }
    public IEnumerator Shake (float duration, float magnitude) {
        float elapsed = 0.0f;

        while (elapsed < duration){
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;
            effectsOffset = new Vector3 (x,y,0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        effectsOffset = new Vector3(0,0,0);
    }

    private float GetLargestNumber (float num1, float num2) {
        if (num1> num2) return num1;
        else return num2;
    }

}
