using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using sharedObjects;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public PlayerStats playerStats;
    public Vector2 spawnPoint;
    private float inputDirection;
    private  bool jump = false;
    private  bool crouch = false;

       void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controller.Move(inputDirection*Time.deltaTime, crouch, jump);
        jump = false;
        animator.SetFloat("Speed", Mathf.Abs(inputDirection));
    }

   

     //Input Event Functions
    public void OnJump(InputAction.CallbackContext context){
        if (context.started && !PauseController.gameIsPaused) {
            jump = true;
            animator.SetBool ("IsJumping", true);
        }
        if (context.canceled && !PauseController.gameIsPaused) {
            jump = false;
        }
    }
    public void OnMoveHorizontal(InputAction.CallbackContext context) {
        if (context.performed && !PauseController.gameIsPaused){
            inputDirection = context.ReadValue<float>() * playerStats.movementSpeed;
        }
        if (context.canceled && !PauseController.gameIsPaused) {
                inputDirection = context.ReadValue<float>();
        }
    }
    public void OnMoveVertical(InputAction.CallbackContext context){
        if (context.performed && !PauseController.gameIsPaused) {
            if (context.ReadValue<float>()==-1){
                    crouch = true;
                }
        }
        if (context.canceled && !PauseController.gameIsPaused) {
            crouch = false;
        }
 
    }
    public void OnLanding (bool landedOnGround){ 
        animator.SetBool ("IsJumping", false);
        if (landedOnGround) this.GetComponentInChildren<PlayerParticleController>().playDustLanding();
        SoundManager.instance.PlaySoundEffect($"Landing{UnityEngine.Random.Range(1,4)}",0.2f);
    }
    public void OnCrouching (bool isCrouching) {
            animator.SetBool("IsCrouching",isCrouching);
    }

}
