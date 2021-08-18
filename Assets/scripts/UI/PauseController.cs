using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
     public static bool gameIsPaused = false;
     public static PauseController instance; 
    public GameObject pauseMenuUI;
    public void OnPause(InputAction.CallbackContext context){
        if  (context.performed){
            if (gameIsPaused) Resume(true);
            else Pause();
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Pause(){
        pauseMenuUI.SetActive(true);
        SoundManager.instance.PlaySoundEffect("Pause");
        SoundManager.instance.PauseSong();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void PauseInput(){
        gameIsPaused = true;
    }
    public void Resume(bool PlaySoundEffect){
        pauseMenuUI.SetActive(false);
        SoundManager.instance.ResumeSong();
        if (PlaySoundEffect) {
            SoundManager.instance.PlaySoundEffect("Pause");
        }
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
