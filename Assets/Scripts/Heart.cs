using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You've got a heart afterall!");
            other.gameObject.GetComponent<PlayerStats>().CollectHeart();
            FindObjectOfType<AudioManager>().Play("DiamondCollect");
            Destroy(gameObject);
        }
    }
}
