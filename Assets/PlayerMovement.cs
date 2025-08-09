using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerControlScheme
    {
        Player1,
        Player2
    }

    // === Constants ===
    //private const string Input_Horizontal = "Horizontal";
    //private const string Input_Jump = "Jump";
    //private const string Input_Crouch = "Crouch";
    private const string Animator_Speed = "Speed";
    private const string Animator_IsJumping = "IsJumping";
    private const string Animator_IsCrouching = "IsCrouching";

    // === Public fields ===
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public PlayerControlScheme controlScheme = PlayerControlScheme.Player1;

    // === Private fields ===
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    void Update()
    {
        // Reset horizontalMove before setting
        horizontalMove = 0f;

        // Điều khiển tùy theo nhân vật
        switch (controlScheme)
        {
            case PlayerControlScheme.Player1: // WASD
                if (Input.GetKey(KeyCode.A)) horizontalMove = -runSpeed;
                if (Input.GetKey(KeyCode.D)) horizontalMove = runSpeed;

                if (Input.GetKeyDown(KeyCode.W))
                {
                    jump = true;
                    animator.SetBool(Animator_IsJumping, true);
                }

                crouch = Input.GetKey(KeyCode.S);
                break;

            case PlayerControlScheme.Player2: // Arrow keys
                if (Input.GetKey(KeyCode.LeftArrow)) horizontalMove = -runSpeed;
                if (Input.GetKey(KeyCode.RightArrow)) horizontalMove = runSpeed;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    jump = true;
                    animator.SetBool(Animator_IsJumping, true);
                }

                crouch = Input.GetKey(KeyCode.DownArrow);
                break;
        }

        animator.SetFloat(Animator_Speed, Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    public void OnLanding()
    {
        animator.SetBool(Animator_IsJumping, false);
    }

    public void OnCrouching(bool IsCrouching)
    {
        animator.SetBool(Animator_IsCrouching, IsCrouching);
    }

    
}
