using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Animator animator;
    public int maxHealth = 100;
    [SerializeField]
    private GameObject spawnItem;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Patrol>().animator;
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        // Play Hit animation
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // Die Animation
        animator.SetBool("IsDead", true);

        // Spawn Heart
        Instantiate(spawnItem, transform.position, Quaternion.identity);

        // Destory the enemy
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Patrol>().enabled = false;
        GetComponent<Pig_Combat>().enabled = false;
        this.enabled = false;
    }
}
