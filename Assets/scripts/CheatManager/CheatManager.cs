using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using sharedObjects;


public class CheatManager : MonoBehaviour
{
    public static CheatManager instance; 
    public TMP_Text cheatDisplay;
    public Text perkDisplay;
    public int cheatLength = 5;
    public GunCheat currentCheat;
    private List<Guns> allGuns;
    private List<CheatInputs> allCheatInputs;

    void Awake()
    {
        if (instance == null){
            instance = this;
        }
 
    }
    void Start() {
        allCheatInputs = Enum.GetValues(typeof(CheatInputs)).Cast<CheatInputs>().ToList();
        allGuns = Enum.GetValues(typeof(Guns)).Cast<Guns>().Where(x => x != Guns.DefaultGun).ToList();
        GenerateNewGunCheatCode();
    }
    void Update()
    {
    }

    void GenerateNewGunCheatCode () {
        currentCheat = new GunCheat(cheatLength);
        for (int i =0; i < cheatLength; i ++){
            currentCheat.cheatCode[i] = allCheatInputs[UnityEngine.Random.Range(0,(allCheatInputs.Count))];
        }
        currentCheat.gun = allGuns[UnityEngine.Random.Range(0,(allGuns.Count))];
        cheatDisplay.text = InputsToString(currentCheat.cheatCode);
        perkDisplay.text = currentCheat.gun.ToString();
    }
    public string InputsToString(CheatInputs[] inputs) {
        String cheatCodeString = "";
        foreach (CheatInputs input in inputs){
            switch (input) {
                case CheatInputs.Shoot: {cheatCodeString += "<sprite=0>"; break;}
                case CheatInputs.Jump: {cheatCodeString += "<sprite=1>"; break;}
                case CheatInputs.Up: {cheatCodeString += "<sprite=2>"; break;}
                case CheatInputs.Down: {cheatCodeString += "<sprite=3>"; break;}
                case CheatInputs.Left: {cheatCodeString += "<sprite=4>"; break;}
                case CheatInputs.Right: {cheatCodeString += "<sprite=5>"; break;}
                default: {cheatCodeString += input.ToString(); break;}
            }
        }
        return cheatCodeString;
    }
    public Guns?  CheckPlayerInput(CheatInputs[] inputs){
        if (Enumerable.SequenceEqual(inputs,currentCheat.cheatCode)) {    
            return currentCheat.gun;
        }
        return null;
    }    
    public IEnumerator SearchForNewCheatCode(int seconds){
        SoundManager.instance.PlaySoundEffect("PowerUp");
        currentCheat = new GunCheat();
        cheatDisplay.text = "";
        perkDisplay.text = "SEARCHING FOR NEW CHEAT CODE...";
        yield return new WaitForSeconds(seconds);
        GenerateNewGunCheatCode();
    }
}
