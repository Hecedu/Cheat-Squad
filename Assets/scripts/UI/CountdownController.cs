using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountdown());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        IEnumerator StartCountdown() {
        PauseController.instance.PauseInput();
        countdownDisplay.text = "";
        yield return new WaitForSeconds(1f);
        while (countdownTime > 0) {
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
