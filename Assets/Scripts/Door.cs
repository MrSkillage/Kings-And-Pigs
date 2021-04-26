using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isExit;
    private EndMenu endMenu;
    Animator animator;
    public PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        endMenu = FindObjectOfType<EndMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            VoiceManager.instance.resetPassword();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.hasPassword() && VoiceManager.instance.HasSaidPasword())
        {
            animator.SetTrigger("Open");
            FindObjectOfType<AudioManager>().Play("Door");
            StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.hasPassword() && VoiceManager.instance.HasSaidPasword())
        {
            animator.SetTrigger("Open");
            FindObjectOfType<AudioManager>().Play("Door"); 
            StartCoroutine(OpenDoor());
        }
    }

    
    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1f);
        if (SceneManager.GetActiveScene().name == "Level_3")
        {
            endMenu.DisplayEndGameHUD();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
