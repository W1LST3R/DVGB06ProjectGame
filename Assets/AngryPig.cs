using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AngryPig : MonoBehaviour
{
    public float speed = -2;
    public float x;
    public int distance;
    public bool left = true;
    public bool playerInsight = false;
    public float slow = 20;
    public bool timeIsRunning = false;
    public bool angerTimerIsRunning = false;
    public bool angry = false;
    public int angryPhase = 10;
    public float time = 0;
    public float getAngryTime = 0;
    public Rigidbody2D rigidbody2D;
    public Rigidbody2D playerBody;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (angerTimerIsRunning)
        {
            if (Mathf.FloorToInt(time % 60) < getAngryTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                angerTimerIsRunning = false;
                time = 0;
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
                timeIsRunning = false ;
                speed = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerInsight)
        {
            float playerPosition = playerBody.position.x;
            float movementDirection = (playerPosition - rigidbody2D.position.x) / slow;
            Debug.Log(movementDirection);
            if (movementDirection < 0 && !left)
            {
                left = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if(movementDirection > 0 && left)
            {
                left = false;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            rigidbody2D.MovePosition(new Vector2((rigidbody2D.position.x + movementDirection)*speed , rigidbody2D.position.y));
        }
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerInsight)
            {
                playerInsight = true;
                speed = 2;
                time = 0;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerInsight)
            {
                speed *= 3;
            }
        }  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInsight = false;
            timeIsRunning = true;
        }     
    }

    private void playerLeftZone()
    {
        
    }
}
