using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter player;
    public float speed;
    public float jumpPower;
    public float downWardDrag;
    public Rigidbody2D playerBody;
    private float move;
    public Animator animator;
    private int jumpCounter;
    private bool hasLeftWall;
    private bool goingLeft;
    private bool playerCanMove;
    private void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        playerCanMove = true;
        goingLeft = false;
        jumpCounter = 0;
        hasLeftWall = true;
    }

    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        checkIfBellowStage();
    }

    private void movePlayer()
    {
        if (playerCanMove)
        {
            move = Input.GetAxis("Horizontal");
            if (move < 0 && goingLeft != true)
            {
                goingLeft = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (move > 0 && goingLeft == true)
            {
                goingLeft = false;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            playerBody.velocity = new Vector2(move * speed, playerBody.velocity.y);
            if (Input.GetButtonDown("Jump") && jumpCounter < 2)
            {
                playerBody.AddForce(new Vector2(playerBody.velocity.x, jumpPower));
                jumpCounter++;
            }
            if (jumpCounter == 2)
            {
                animator.SetBool("IsDubbelJumping", true);
            }
            else
            {
                animator.SetBool("IsDubbelJumping", false);

            }
        }
        animator.SetFloat("Horizontal", playerBody.velocity.x);
        animator.SetFloat("Vertical", playerBody.velocity.y);
        animator.SetFloat("Speed", playerBody.velocity.sqrMagnitude);
    }

    private void checkIfBellowStage()
    {
        if (transform.position.y < -5.56)
        {
            transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        }
    }


    private void jumpCounterReset()
    {
        jumpCounter = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall"))
        {
            jumpCounterReset();
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            playerCanMove = false;
            player = null;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (WallScript.touchingWall)
        {
            animator.SetBool("IsTouchingWall", WallScript.touchingWall);
            if (hasLeftWall)
            {
                jumpCounterReset();
                hasLeftWall = false;    
            }
            playerBody.velocity = new Vector2(playerBody.velocity.x, downWardDrag);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!WallScript.touchingWall)
        {
            hasLeftWall = true;
            animator.SetBool("IsTouchingWall", WallScript.touchingWall);
        }
    }

    public void playerDied()
    {
        playerBody.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
    }
}



