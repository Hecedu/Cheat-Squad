using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public static CountdownController instance;
    public Text countdownDisplay;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator StartCountdown(int countdownTime) {
        var playerlist = GameController.instance.playerInitDataList;
        PauseController.instance.PauseInput();
        countdownDisplay.text = "";
        yield return new WaitForSeconds(1f);
        int index = 0;
        while (countdownTime > 0) {
            index++;
            if (countdownTime >= playerlist.Count) {
                CameraController.instance.ChangePlayerCameraTarget(playerlist[index-1].playerNumber);
                CameraController.instance.ChangeCameraTarget(sharedObjects.CameraTargets.Player);
                CameraController.instance.ChangeZoom(CameraController.pixelPerfectZoomInCameraSize);
            }
           else {
                CameraController.instance.ChangeCameraTarget(sharedObjects.CameraTargets.ActivePlayers);
                CameraController.instance.ChangeZoom(CameraController.pixelPerfectDefaultCameraSize);
           }

            SoundManager.instance.PlaySoundEffect("Tick1",0.5f);
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
 
            countdownTime--;
        }
        PauseController.instance.Resume(false);
        SoundManager.instance.PlaySoundEffect("Tick2",0.5f);

        countdownDisplay.text = "LETS GO!!";
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlaySong("4cIcedTea",0.2f);
        countdownDisplay.enabled = false;

    }
}
