using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDmg = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            if (enemy.GetComponent<Unit>())
            {
                enemy.GetComponent<Unit>().takeDamage(attackDmg);
            } else
            {
                enemy.GetComponent<Diamond>().TakeDamage(attackDmg);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
