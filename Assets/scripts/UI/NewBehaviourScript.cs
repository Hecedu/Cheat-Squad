using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public void OnPause(InputAction.CallbackContext context){
        if (gameIsPaused){
            Resume();
        }
        else {
            Pause();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

}
