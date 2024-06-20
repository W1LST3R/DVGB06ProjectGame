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
}
