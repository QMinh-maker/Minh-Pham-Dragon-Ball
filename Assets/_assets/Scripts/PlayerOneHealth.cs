using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 1000;
    int currentHealth;

    public HealthBar leftHealthBar;
    void Start()
    {
        currentHealth = maxHealth;
        leftHealthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        leftHealthBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Nguoi choi chet.");
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Tắt các script điều khiển 
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this)
                script.enabled = false;
        }

        // Gọi hàm biến mất sau 1.5s (thời gian để animation chết chạy xong)
        Invoke(nameof(Disappear), 1.0f);
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
