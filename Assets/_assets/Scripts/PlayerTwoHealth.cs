using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 1000;
    int currentHealth;

    public HealthBar RightHealthBar;


    void Start()
    {
        currentHealth = maxHealth;
        RightHealthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        RightHealthBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Ke dich chet.");
        animator.SetBool("IsDead", true);

        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        // Tắt các script điều khiển khác nếu cần
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this)
                script.enabled = false;
        }

        // Gọi hàm biến mất sau 1.5s (thời gian để animation chết chạy xong)
        Invoke(nameof(Disappear), 5.0f);
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
