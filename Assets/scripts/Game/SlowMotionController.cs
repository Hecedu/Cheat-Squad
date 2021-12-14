using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowMotionController : MonoBehaviour
{
    public static SlowMotionController instance;
    // Start is called before the first frame update
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator StartMatchEndSlowMotion(float seconds) {
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator StartHitStun(float seconds, GameObject bullet) {
        if (PauseController.instance != null) PauseController.instance.PauseInput();
        Time.timeScale = 0.01f;
        StartCoroutine(CameraController.instance.Shake(seconds,0.2f));
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
        if (PauseController.instance != null) PauseController.instance.Resume(false);
        Destroy(bullet);
    }
}
