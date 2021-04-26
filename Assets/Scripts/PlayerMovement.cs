using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float speed = 40f;
    public float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Password: " + GameManager.instance.getPassword());
            Debug.Log("Player Knows Password: " + GameManager.instance.hasPassword());
        }

    }
    
    public void Jump()
    {
        jump = true;
        animator.SetBool("IsJumping?", true);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping?", false);
    }

    private void FixedUpdate()
    {
        // Move ourcharacter 
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;


    }
}
