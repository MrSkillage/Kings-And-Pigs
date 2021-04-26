using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_Combat : MonoBehaviour
{
    public int attackDmg = 1;
    public Transform attackPoint;
    public float attackRange = 0.3f;
    public LayerMask playerLayer;
    public bool _Attacking;
    Collider2D player;
    Animator animator;


    // Attack Rate
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _Attacking = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (Time.time >= nextAttackTime) {
            if (player)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    IEnumerator Attack()
    {
        if (!_Attacking)
        {
            _Attacking = true;
            // Play attack animation
            animator.SetTrigger("Attack");
            // Damage Player
            player.GetComponent<PlayerStats>().TakeDamage(attackDmg);
            FindObjectOfType<AudioManager>().Play("EnemyHit");
            yield return new WaitForSeconds(1f);
            _Attacking = false;
        }        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
