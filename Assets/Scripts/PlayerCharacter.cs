    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool bellowStage = false;
    private bool enemyDead = false;
    private bool wallSliding = false;
    public float wallJumpPower;
    private bool touchingWall = false;
    private bool hasWallJumped = true;
    private GameObject spawnPoint;

    private void Start()
    {
        //Sets the virabels for the player
        spawnPoint = GameObject.FindGameObjectWithTag("Start");
        transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y-0.98f, spawnPoint.transform.position.z);
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
    }
    private void FixedUpdate()
    {
        //Makes the player slowly glide down the wall
        if (wallSliding)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Clamp(playerBody.velocity.y, -downWardDrag, float.MaxValue));
        }
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
            if (wallSliding)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Clamp(playerBody.velocity.y, -downWardDrag, float.MaxValue));
            }
            //if the player press space it will jump up too two times
            if (Input.GetButtonDown("Jump") && jumpCounter < 2 && !touchingWall)
            {
                if (!hasWallJumped)
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, jumpPower * wallJumpPower);
                    //playerBody.velocity = new Vector2((move*-1) * speed, playerBody.velocity.y);
                    hasWallJumped = true;
                }
                else playerBody.velocity = new Vector2(playerBody.velocity.x, jumpPower);

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
        }else playerBody.velocity = Vector2.zero;
        
        //Sets the players animation
        animator.SetFloat("Horizontal", playerBody.velocity.x);
        animator.SetFloat("Vertical", playerBody.velocity.y);
        animator.SetFloat("Speed", playerBody.velocity.sqrMagnitude);
    }

    //Resets jump counter
    private void jumpCounterReset()
    {
        jumpCounter = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.tag.Equals("SpawnPoint"))
        {
            audioManager.playSFX(audioManager.finish);
            spawnPoint = collisionObject;
            spawnPoint.GetComponent<CheckPoint>().checkPointMade();
            spawnPoint.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Chooses right dependent on what the player hit
        
        GameObject collisionObject = collision.gameObject;
        switch (collisionObject.tag)
        {
            case "EnemyAP":
                if(playerBody.position.y > collisionObject.GetComponent<Rigidbody2D>().position.y + 0.6)
                {
                    jumpCounterReset();
                    collisionObject.GetComponent<AngryPig>().die();
                }
                else
                {
                    StartCoroutine(playerDied(collisionObject));
                }
                break;
            case "Trap":
                StartCoroutine(playerDied());
                break;
            case "EnemyT":
                if (playerBody.position.y > collisionObject.GetComponent<Rigidbody2D>().position.y + 0.6)
                {
                    jumpCounterReset();
                    collisionObject.GetComponent<Trunk>().die();
                }
                else
                {
                    StartCoroutine(playerDied(collisionObject));
                }
                break;
            case "Wall":
                audioManager.playSFX(audioManager.wallJump);
                break;
            case "Finish":
                audioManager.playSFX(audioManager.finish);
                playerCanMove = false;
                player = null;
                break;
            case "Abyss":
                bellowStage = true;
                StartCoroutine(playerDied());
                break;
            case "Start":
                jumpCounterReset();
                playerCanMove = true;

                break;
            case "NotFloor":
                audioManager.playSFX(audioManager.wallJump);
                break;
            default:
                hasWallJumped = true;
                audioManager.playSFX(audioManager.wallJump);
                jumpCounterReset();
                break;
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
            touchingWall = true;
            wallSliding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //If the player leaves the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            hasWallJumped = false;
            touchingWall = false;
            wallSliding = false;
            hasLeftWall = true;
            animator.SetBool("IsTouchingWall", false);
        }
    }

    //if the player dies
    //sets player position to start position

    
    //Kills the player
    IEnumerator playerDied()
    {
        playerCanMove = false;
        jumpCounterReset();
        audioManager.playSFX(audioManager.death);
        if (!bellowStage)
        {
            animator.SetTrigger("IsDead");
            yield return new WaitForSeconds(0.6f);
            animator.SetTrigger("IsAlive");
        }
        playerBody.transform.position = spawnPoint.transform.position;
        playerCanMove = true;
        bellowStage = false;
    }

    //Kills the player
    IEnumerator playerDied(GameObject enemy)
    {
        if (enemy.tag.Equals("EnemyAP"))
        {
            playerCanMove = false;
            AngryPig angryPig =enemy.GetComponent<AngryPig>();
            GameObject parent = enemy.transform.parent.gameObject;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            audioManager.playSFX(audioManager.death);
            animator.SetTrigger("IsDead");
            yield return new WaitForSeconds(0.6f);
            animator.SetTrigger("IsAlive");
            angryPig.cantAttack();
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            jumpCounterReset();
            playerBody.transform.position = spawnPoint.transform.position;
            angryPig.playerLeftZone();
            playerCanMove = true;
            angryPig.canAttack();
        }
        else
        {
            Trunk trunk =enemy.GetComponent<Trunk>();
            GameObject parent = enemy.transform.parent.gameObject;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            playerCanMove = false;
            audioManager.playSFX(audioManager.death);
            animator.SetTrigger("IsDead");
            yield return new WaitForSeconds(0.6f);
            animator.SetTrigger("IsAlive");
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            jumpCounterReset();
            playerBody.transform.position = spawnPoint.transform.position;
            trunk.playerLeftZone();
            playerCanMove = true;
        }

        
    }
}



