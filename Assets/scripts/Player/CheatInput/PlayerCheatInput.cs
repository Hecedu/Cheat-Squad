using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using sharedObjects;

public class PlayerCheatInput : MonoBehaviour
{
    public PlayerStats playerStats;
    private Queue<CheatInputs> inputQueue = new Queue<CheatInputs>();

    public void OnShoot(InputAction.CallbackContext context) {
        if (context.started && !PauseController.gameIsPaused){
            AddToInputQueue(CheatInputs.Shoot);
        }

    }
    public void OnJump(InputAction.CallbackContext context){
        if (context.started && !PauseController.gameIsPaused){
            AddToInputQueue(CheatInputs.Jump);
        }
    }
    public void OnMoveHorizontal(InputAction.CallbackContext context) {
        if (context.performed && !PauseController.gameIsPaused){
            Vector2 direction = new Vector2(context.ReadValue<float>(),0);
            if (DirectionToInput(direction) != null) {
                AddToInputQueue(DirectionToInput(direction).Value);
            }
        }
    }
    public void OnMoveVertical(InputAction.CallbackContext context) {
        if (context.performed && !PauseController.gameIsPaused){
            Vector2 direction = new Vector2(0,context.ReadValue<float>());
            if (DirectionToInput(direction) != null) {
                AddToInputQueue(DirectionToInput(direction).Value);
            }
        }
    }
    void Start()
    {
    }
    void Update()
    {
        playerStats.playerInputString = CheatManager.instance.InputsToString(inputQueue.ToArray());
    }

    public void AddToInputQueue (CheatInputs input) {
         if (inputQueue.Count == CheatManager.instance.cheatLength) {
            inputQueue.Dequeue();
        }
        inputQueue.Enqueue(input);
        if (CheatManager.instance.CompareInputsWithCheatCode(inputQueue.ToArray())) {
            //execute perk code
        }
    } 
    public CheatInputs? DirectionToInput (Vector2 direction) {
        if (direction == Vector2.down) {
            return CheatInputs.Down;
        }
        if (direction == Vector2.up) {
            return CheatInputs.Up;  
        }
        if (direction == Vector2.left) {
            return CheatInputs.Left;
        }
        if (direction == Vector2.right) {
            return CheatInputs.Right;
        } 
        else return null;
    }
}
