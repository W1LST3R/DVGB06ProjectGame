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
    AudioManager audioManager;

    private void Start()
    {
        //Sets the virabels for the player
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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            //Flips the player dependent of wich way the player is moving in
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
            //Moves the player on the x axis
            playerBody.velocity = new Vector2(move * speed, playerBody.velocity.y);
            //if the player press space it will jump up too two times
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
        //Sets the players animation
        animator.SetFloat("Horizontal", playerBody.velocity.x);
        animator.SetFloat("Vertical", playerBody.velocity.y);
        animator.SetFloat("Speed", playerBody.velocity.sqrMagnitude);
    }

    //Checks if the player have fallen of the stage
    private void checkIfBellowStage()
    {
        if (transform.position.y < -5.56)
        {
            playerDied();
        }
    }

    //Resets jump counter
    private void jumpCounterReset()
    {
        jumpCounter = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.playSFX(audioManager.wallJump);
        //if the player collides with every thing but a wall
        if (!collision.gameObject.CompareTag("Wall"))
        {
            jumpCounterReset();
        }
        //When the player collides with the finish
        if (collision.gameObject.CompareTag("Finish"))
        {
            playerCanMove = false;
            player = null;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //if player i touching a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            animator.SetBool("IsTouchingWall", true);
            //resets jump if player has left the wall
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
        //If the player leaves the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            audioManager.playSFX(audioManager.wallJump);
            hasLeftWall = true;
            animator.SetBool("IsTouchingWall", false);
        }
    }

    //if the player dies
    //sets player position to start position
    public void playerDied()
    {
        audioManager.playSFX(audioManager.death);
        playerBody.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
    }
}



