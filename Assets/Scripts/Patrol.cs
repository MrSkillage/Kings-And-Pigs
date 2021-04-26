using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool isMovingRight = true;
    public Transform groundDetection;
    public Animator animator;

    void Update()
    {
        var movement = Vector2.right * speed * Time.deltaTime;
        transform.Translate(movement);
        animator.SetFloat("Speed", Mathf.Abs(speed));
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            if (isMovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
        }
    }
}
