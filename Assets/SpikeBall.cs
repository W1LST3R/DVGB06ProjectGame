using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public bool start = false;
    void Start()
    {
       startSwing();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            startSwing();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
            startSwing();
        }else if (collision.gameObject.CompareTag("SpikeBall"))
        {
            startSwing();
        }
        
    }
    private void startSwing()
    {
        rb.velocity = Vector2.one * 15;
    }
}
