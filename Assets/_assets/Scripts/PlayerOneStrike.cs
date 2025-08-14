using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneStrike : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;


    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxcomboDelay = 1;

     void Start()
    {
        animator = GetComponent<Animator>();
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Strike();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Strike()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            animator.SetBool("Strike1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks,0,3);

        if(noOfClicks >=2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            animator.SetBool("Strike1", false);
            animator.SetBool("Strike2", true);
        }

        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Strike2"))
        {
            animator.SetBool("Strike2", false);
            animator.SetBool("Strike3", true);
        }

        if (noOfClicks >= 4 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Strike3"))
        {
            animator.SetBool("Strike3", false);
            animator.SetBool("Strike4", true);
        }




        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,
            attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerTwoHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
