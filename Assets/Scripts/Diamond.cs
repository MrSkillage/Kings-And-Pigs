using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diamond : MonoBehaviour
{
    public char letter;
    public int letterIndex;
    [SerializeField]
    private GameObject floatingTextPrefab;
    private float secondsToDestory = 1f;

    Animator animator;
    int maxHealth = 50;
    int currentHealth;
    int value = 1;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play Hit Animation
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        GameManager.instance.UpdateScore(value);
        ShowLetter();
        GameManager.instance.UpdatePassword(letter, letterIndex);
        Destroy(this.gameObject);
        FindObjectOfType<AudioManager>().Play("DiamondCollect");
    }

    void ShowLetter()
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = this.letter.ToString();
            Destroy(prefab, secondsToDestory);
        }


    }

}
