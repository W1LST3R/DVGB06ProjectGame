using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 1;
    public float jumpPower = 2;
    public Rigidbody2D rigidbody2D;
    public float move;
    public Animator animator;
    public int jumpCounter = 0;
    public bool hasJumped;

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        move = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);
        if (Input.GetButtonDown("Jump") && jumpCounter < 2)
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x,jumpPower));
            jumpCounter++;
        }
        animator.SetFloat("Horizontal", rigidbody2D.velocity.x);
        animator.SetFloat("Vertical", rigidbody2D.velocity.y);
        animator.SetFloat("Speed", rigidbody2D.velocity.sqrMagnitude);
    }


    private void jumpCounterReset()
    {
        jumpCounter = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCounterReset();
    }
}



