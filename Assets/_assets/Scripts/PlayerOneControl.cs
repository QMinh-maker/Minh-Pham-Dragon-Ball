using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneControl : MonoBehaviour
{
    public CharacterController2D character;
    public Animator animator;
    public float Speed = 40f;


    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal") * Speed; // A/D hoặc ←/→
        float moveY = Input.GetAxisRaw("Vertical") * Speed;   // W/S hoặc ↑/↓
        character.Move(moveX, moveY);

        Transform enemy = character.enemy;
        if (enemy != null)
        {
            float playerX = transform.position.x;
            float enemyX = enemy.position.x;

            // Kiểm tra hướng di chuyển so với vị trí enemy
            bool isForward = ((moveX > 0 && playerX < enemyX) || (moveX < 0 && playerX > enemyX));
            bool isBackward = ((moveX > 0 && playerX > enemyX) || (moveX < 0 && playerX < enemyX));

            bool isFlying = (moveY > 0);
            bool isLanding = (moveY < 0);

            animator.SetBool("isForward", isForward);
            animator.SetBool("isBackward", isBackward);
            animator.SetBool("isFlying", isFlying);
            animator.SetBool("isLanding", isLanding);


        }
    }
}
