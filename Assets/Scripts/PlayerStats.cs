using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    Animator animator;
    public EndMenu menu;

    int maxHealth = 3;
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {

        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health > numOfHearts) health = numOfHearts;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
            
            if (i < numOfHearts)
                hearts[i].enabled = true;
            else            
                hearts[i].enabled = false;
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        // Play Hit Animation
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Die();
        }
    }

    public void CollectHeart()
    {
        health += 1;
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        menu.DisplayEndGameHUD();
        Time.timeScale = 0;
        Debug.Log("You have Died!");
    }

}
