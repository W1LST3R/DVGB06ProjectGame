using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AngryPig : MonoBehaviour
{
    public float speed = -2;
    private bool left = true;
    private bool inSight = false;
    public float slow = 20;
    private bool timeIsRunning = false;
    private bool angerTimerIsRunning = false;
    private bool angry = false;
    public float angryPhase = 2;
    private float time = 0;
    public float cooldownTime = 0;
    public float getAngryTime = 0;
    public Rigidbody2D enemyBody;
    private Rigidbody2D playerBody;
    public Rigidbody2D head;
    public Animator animator;
    public float movementDirection;
    private AudioManager audioManager;
    private bool attack = true;
    private bool stayPlace = false;

    private void Awake()
    {
        //Gets the AúdioManager component
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        //if the player is in the enemys radius the timmer will start
        if (angerTimerIsRunning )
        {
            //counts up the time
            if (Mathf.FloorToInt(time % 60) < getAngryTime)
            {
                time += Time.deltaTime;
            }
            //Increases the speed of the enemy
            else if (speed != 0)
            {
                angerTimerIsRunning = false;
                speed = 4;
                time = 0;
                angry = true;
            }
        }
        //if the player is outside of the enemys radius
        if (timeIsRunning)
        {
            //when the time is equal to the tresh hold the enemy will lose agro of the player
            if(Mathf.FloorToInt(cooldownTime % 60) < angryPhase)
            {
                cooldownTime += Time.deltaTime;
            }
            else
            {
                playerLeftZone();
            }
        }

        //if the player is in the radius of the enemy it will move towards the enemys location
        if (inSight && attack)
        {
            float playerPosition = playerBody.position.x;
            float enemyPosition = enemyBody.position.x;
            //gets the distans between the player and enemy and devides it so the enemy dont teleport.
            //Note could improve this so the enemy move more smooth
            movementDirection = (playerPosition - enemyPosition) / slow;
            float result = Mathf.Sqrt(movementDirection * movementDirection);
            if(result < 0.01f)
            {
                if(movementDirection < 0)
                {
                    movementDirection += -0.01f;
                }
                else
                {
                    movementDirection += 0.01f;
                }
            }
            //flips the enemy dependent on wich way it is going
            if (movementDirection < 0 && !left)
            {
                left = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (movementDirection > 0 && left)
            {
                left = false;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            movementDirection *= speed;
            //Moves the enemy
            enemyBody.MovePosition(new Vector2(enemyPosition + movementDirection, enemyBody.position.y));
        }
        if (stayPlace)
        {
            enemyBody.velocity = Vector2.zero;
        }
        //Sets the animation for the enemy
        animator.SetFloat("Horizontal", movementDirection);
        animator.SetFloat("Speed", speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyBody.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player enters the enemys radius
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!inSight)
            {
                playerInSight();
            }
        }
        //If enemy falls of stage
        else if(collision.gameObject.CompareTag("Abyss"))
        {
          die();  
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if the player stays in the enemys radius
        if (collision.gameObject.CompareTag("Player"))
        {
            if (inSight && !angry)
            {
                angerTimerIsRunning = true;
            }else if (!inSight)
            {
                playerInSight();
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if the player exits the enemys radius
        if (collision.gameObject.CompareTag("Player"))
        {
            timeIsRunning = true;
        }     
    }

    //sets necessary variabels for the enemy
    private void playerInSight()
    {
        inSight = true;
        speed = 2;
        time = 0;
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    //sets the virabels so the enemy cant move
    public void playerLeftZone()
    {
        angry = false;
        inSight = false;
        timeIsRunning = false;
        angerTimerIsRunning = false;
        speed = 0;
        playerBody = null;
        movementDirection = 0;
        time = 0;
        cooldownTime = 0;
}
    //Kills the enemy
    public void die()
    {
        cantAttack();
        StartCoroutine(playDeath());
    }

    //Plays death animation, then destroys gameobject
    IEnumerator playDeath()
    {
        audioManager.playSFX(audioManager.enemyDeath);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject.transform.parent.gameObject);
    }

    //Sets so it can attack
    public void canAttack()
    {
        attack = true;
        stayPlace = false;
    }
    //Makes so it cant attack
    public void cantAttack()
    {
        attack = false;
        stayPlace = true;
    }
}
