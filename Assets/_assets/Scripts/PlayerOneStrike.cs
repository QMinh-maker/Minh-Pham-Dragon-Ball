using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneStrike : MonoBehaviour
{
    public Animator anim;
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
        anim = GetComponent<Animator>();
    }

   

    // Update is called once per frame
    void Update()
    {
       

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f 
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Strike1")) 
        {
            anim.SetBool("Strike1", false);
            Debug.Log("strike 1 dang chay");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f 
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Strike2")) 
        {
            anim.SetBool("Strike2", false);
            Debug.Log("strike 2 dang chay");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f 
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Strike3")) 
        { 
            anim.SetBool("Strike3", false);
            Debug.Log("strike 3 dang chay");

        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Strike4"))
        {
            anim.SetBool("Strike4", false);
            Debug.Log("strike 4 dang chay");
            noOfClicks = 0;
        }
        if (Time.time - lastClickedTime > maxcomboDelay) 
        { 
            noOfClicks = 0; 
        }

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
            anim.SetBool("Strike1", true);
            
        }
        noOfClicks = Mathf.Clamp(noOfClicks,0,3);

        if(noOfClicks ==2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            anim.SetBool("Strike1", false);
            anim.SetBool("Strike2", true);
            
        }

        if (noOfClicks == 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Strike2"))
        {
            anim.SetBool("Strike2", false);
            anim.SetBool("Strike3", true);
            
        }

        if (noOfClicks == 4 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Strike3"))
        {
            anim.SetBool("Strike3", false);
            anim.SetBool("Strike4", true);
            
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
