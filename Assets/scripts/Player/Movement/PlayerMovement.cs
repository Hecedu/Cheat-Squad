using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public PlayerStats playerStats;
    public Vector2 spawnPoint;
    private float inputDirection;
    private  bool jump = false;
    private  bool crouch = false;
    float xBound = 11.5f;
    float yBound = 6f;
    // Start is called before the first frame update
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
    public void OnLanding (){ 
        animator.SetBool ("IsJumping", false);
        SoundManager.instance.PlaySoundEffect($"Landing{UnityEngine.Random.Range(1,4)}",0.2f);
    }
    public void OnCrouching (bool isCrouching) {
            animator.SetBool("IsCrouching",isCrouching);
    }
    void Start()
    {
        spawnPoint = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        controller.Move(inputDirection*Time.deltaTime, crouch, jump);
        jump = false;
        CheckOutOfBounds();
        animator.SetFloat("Speed", Mathf.Abs(inputDirection));
    }

    public void CheckOutOfBounds(){
        if (this.transform.position.x > xBound || this.transform.position.x < -xBound|| this.transform.position.y > yBound || this.transform.position.y < -yBound) {
            SoundManager.instance.PlaySoundEffect($"Death{UnityEngine.Random.Range(1,4)}",0.2f);
            this.playerStats.lives --;
            this.transform.position = spawnPoint;
        }
    }
}