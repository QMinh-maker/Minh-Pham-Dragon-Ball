using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Animator animator;
    private int comboStep = 0;
    private float lastClickTime = 0f;
    public float comboDelay = 1f;

    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    [Header("Debug")]
    public bool showAttackRangeInGame = true;  // Bật/tắt hiển thị vòng tròn trong Game view

    void Update()
    {
        // Reset combo nếu quá lâu không click
        if (Time.time - lastClickTime > comboDelay)
        {
            comboStep = 0;
        }

        // Click chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            lastClickTime = Time.time;
            comboStep++;

            if (comboStep > 4) comboStep = 1;

            animator.SetInteger("ComboStep", comboStep);
            animator.SetTrigger("Attack");
        }

        // Tìm kẻ địch trong vùng tấn công
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position, attackRange, enemyLayers
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerTwoHealth>().TakeDamage(attackDamage);
        }
    }

    // Hiện vòng tròn vùng tấn công trong Scene view
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Tùy chọn hiện vòng tròn trong Game view
    void OnDrawGizmos()
    {
        if (showAttackRangeInGame && attackPoint != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f); // đỏ nhạt
            Gizmos.DrawSphere(attackPoint.position, attackRange);
        }
    }
}




