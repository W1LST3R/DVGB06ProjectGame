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
    public bool timeIsRunning = false;
    public int angryPhase = 10;
    public float time = 0;
    public Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void FixedUpdate()
    {
        if (left)
        {
            x--;
            if (x < -distance)
            {
                left = false;
                speed *= -1;
            }
        }
        else
        {
            x++;
            if (x > distance)
            {
                left = true;
                speed *= -1;
            }
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerInsight)
            {
                playerInsight = true;
                speed *= 3;
                distance = 30;
                time = 0;
            }
        }  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerLeftZone();
        }     
    }

    private void playerLeftZone()
    {
        if(playerInsight)
        {
            while(Mathf.FloorToInt(time%60) < angryPhase)
            {
                time += Time.deltaTime;
            }
            speed /= 3;
            playerInsight = false;
            distance = 50;
           
        }
    }
}
