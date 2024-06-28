using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public Animator animator;
    public float movementDirection;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (angerTimerIsRunning )
        {
            if (Mathf.FloorToInt(time % 60) < getAngryTime)
            {
                time += Time.deltaTime;
            }
            else if (speed != 0)
            {
                angerTimerIsRunning = false;
                speed = 4;
                time = 0;
                angry = true;
            }
        }

        if (timeIsRunning)
        {
            if(Mathf.FloorToInt(time % 60) < angryPhase)
            {
                time += Time.deltaTime;
            }
            else
            {
                playerLeftZone();
            }
        }

        if (inSight)
        {
            float playerPosition = playerBody.position.x;
            float enemyPosition = enemyBody.position.x;
            movementDirection = (playerPosition - enemyPosition) / slow;
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

            enemyBody.MovePosition(new Vector2(enemyPosition + movementDirection, enemyBody.position.y));
            
        }
        animator.SetFloat("Horizontal", movementDirection);
        animator.SetFloat("Speed", speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (DeathScript.isDead)
            {
                Destroy(gameObject);
                DeathScript.isDead = false;
                Debug.Log("död");

            }
            else if (!DeathScript.isDead)
            {
                Debug.Log("inte död");

                playerLeftZone();
                PlayerCharacter.player.playerDied();
            }

        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!inSight)
            {
                playerInSight();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            timeIsRunning = true;
        }     
    }

    private void playerInSight()
    {
        inSight = true;
        speed = 2;
        time = 0;
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void playerLeftZone()
    {
        angry = false;
        inSight = false;
        timeIsRunning = false;
        speed = 0;
        playerBody = null;
        movementDirection = 0;
    }
}
