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
    public Cheat currentCheat;

    void Awake()
    {
        if (instance == null){
            instance = this;
        }
        GenerateNewCheatCode(); 
    }

    void Update()
    {
    }

    void GenerateNewCheatCode () {
        currentCheat = new Cheat(cheatLength);
        CheatInputs[] allCheatInputs = (CheatInputs[])Enum.GetValues(typeof(CheatInputs));
        Perks[] allPerks = (Perks[])Enum.GetValues(typeof(CheatInputs)); 
        for (int i =0; i < cheatLength; i ++){
            currentCheat.cheatCode[i] = allCheatInputs[UnityEngine.Random.Range(0,(allCheatInputs.Length-1))];
        }
        currentCheat.perk = allPerks[UnityEngine.Random.Range(0,(allPerks.Length-1))];
        cheatDisplay.text = InputsToString(currentCheat.cheatCode);
        perkDisplay.text = currentCheat.perk.ToString();
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
    public bool CompareInputsWithCheatCode(CheatInputs[] inputs){
        if (Enumerable.SequenceEqual(inputs,currentCheat.cheatCode)) {
            StartCoroutine(SearchForNewCheatCode(4));
            return true;
        }
        return false;
    }    
    IEnumerator SearchForNewCheatCode(int seconds){
        SoundManager.instance.PlaySoundEffect("PowerUp");
        currentCheat = new Cheat();
        cheatDisplay.text = "";
        perkDisplay.text = "SEARCHING FOR NEW CHEAT CODE...";
        yield return new WaitForSeconds(seconds);
        GenerateNewCheatCode();
    }
}
