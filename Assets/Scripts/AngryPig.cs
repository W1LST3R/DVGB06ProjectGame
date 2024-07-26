using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AngryPig : MonoBehaviour
{
    public float speed = -2;
    public bool left = true;
    public bool inSight = false;
    public float slow = 20;
    public bool timeIsRunning = false;
    public bool angerTimerIsRunning = false;
    public bool angry = false;
    public float angryPhase = 10;
    public float time = 0;
    public float getAngryTime = 0;
    public Rigidbody2D enemyBody;
    private Rigidbody2D playerBody;
    public Rigidbody2D head;
    public Animator animator;
    public float movementDirection;
    private AudioManager audioManager;
    private float headDiff;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        headDiff = head.position.y - enemyBody.position.y;
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
            if(Mathf.FloorToInt(time % 60) < angryPhase)
            {
                time += Time.deltaTime;
            }
            else
            {
                playerLeftZone();
            }
        }

        //if the player is in the radius of the enemy it will move towards the enemys location
        if (inSight)
        {
            float playerPosition = playerBody.position.x;
            float enemyPosition = enemyBody.position.x;
            //gets the distans between the player and enemy and devides it so the enemy dont teleport.
            //Note could improve this so the enemy move more smooth
            movementDirection = (playerPosition - enemyPosition) / slow;
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
            head.MovePosition(new Vector2(enemyBody.position.x, enemyBody.position.y + headDiff));


        }
        //Sets the animation for the enemy
        animator.SetFloat("Horizontal", movementDirection);
        animator.SetFloat("Speed", speed);
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
        else if(collision.gameObject.CompareTag("Abyss"))
        {
          StartCoroutine(enemyDead());  
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
    IEnumerator enemyDead()
    {
        audioManager.playSFX(audioManager.enemyDeath);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject.transform.parent.gameObject);
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
        speed = 0;
        playerBody = null;
        movementDirection = 0;
    }

    public void die()
    {
        StartCoroutine(playDeath());
    }

    IEnumerator playDeath()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        audioManager.playSFX(audioManager.enemyDeath);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
