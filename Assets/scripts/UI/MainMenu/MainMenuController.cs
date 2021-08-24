using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton(){
        SceneManager.LoadScene("TestLevel");
    }
    public void CreditsButton(){
        credits.SetActive(true);
    }
    public void ExitButton(){
        Application.Quit();
    }
    public void ReturnButton(){
        credits.SetActive(false);
    }
}
