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
    private bool playerDead = false;
    private bool enemyDead = false;
    private bool wallSliding = false;
    public float wallJumpPower;
    private bool touchingWall = false;
    private bool hasWallJumped = true;

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
    }
    private void FixedUpdate()
    {
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
            if (Input.GetButtonDown("Jump") && jumpCounter < 2)
            {
                if (!hasWallJumped)
                {
                    playerBody.AddForce(new Vector2(playerBody.velocity.x, jumpPower * wallJumpPower));
                    //playerBody.velocity = new Vector2((move*-1) * speed, playerBody.velocity.y);
                    hasWallJumped = true;
                }
                else playerBody.AddForce(new Vector2(playerBody.velocity.x, jumpPower));

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

    //Resets jump counter
    private void jumpCounterReset()
    {
        jumpCounter = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Trap"))
        {
           // bellowStage = true;
            StartCoroutine(playerDied());
        }
           
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag)
        {
            case "Head":
                if (!enemyDead)
                {
                    jumpCounterReset();
                    GameObject parent = collisionObject.transform.parent.gameObject;
                    StartCoroutine(killEnemy(parent));
                }
                break;
            case "EnemyAP":
                if (!playerDead && collisionObject != null)
                {
                    GameObject parent = collisionObject.transform.parent.gameObject;
                    PolygonCollider2D enemyHead = parent.GetComponentInChildren<PolygonCollider2D>();
                    enemyHead.enabled = false;
                    collisionObject.GetComponent<AngryPig>().playerLeftZone();
                    StartCoroutine(playerDied(enemyHead));
                } 
                break;
            case "Trap":
                if (!playerDead)
                {
                    StartCoroutine(playerDied());
                }
                break;
            case "EnemyT":
                if (!playerDead && collisionObject != null)
                {
                    GameObject parent = collisionObject.transform.parent.gameObject;
                    PolygonCollider2D enemyHead = parent.GetComponentInChildren<PolygonCollider2D>();
                    enemyHead.enabled = false;
                    collisionObject.GetComponent<Trunk>().playerLeftZone();
                    StartCoroutine(playerDied(enemyHead));
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
                if (!playerDead)
                {
                    StartCoroutine(playerDied());
                }
                break;
            case "Start":
                jumpCounterReset();
                playerCanMove = true;

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

    
    IEnumerator playerDied()
    {
        playerDead = true;
        playerCanMove = false;
        audioManager.playSFX(audioManager.death);
        if (!bellowStage)
        {
            animator.SetTrigger("IsDead");
            yield return new WaitForSeconds(0.6f);
            animator.SetTrigger("IsAlive");
        }
        playerBody.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        playerCanMove = true;
        bellowStage = false;
        playerDead = false;

    }

    IEnumerator playerDied(PolygonCollider2D enemyHead)
    {
        playerDead = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        playerCanMove = false;
        audioManager.playSFX(audioManager.death);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("IsAlive");
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        playerBody.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        jumpCounterReset();
        enemyHead.enabled = true;
        playerCanMove = true;
        playerDead = false;
    }

    IEnumerator killEnemy(GameObject parent)
    {
        enemyDead = true;
        parent.GetComponentInChildren<EdgeCollider2D>().enabled = false;
        audioManager.playSFX(audioManager.enemyDeath);
        parent.GetComponentInChildren<Animator>().SetTrigger("IsDead");
        yield return new WaitForSeconds(0.4f);
        Destroy(parent);
        enemyDead = false;
    }
}



