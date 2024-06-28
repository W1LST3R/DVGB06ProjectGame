using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
       startSwing();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player hits the spike ball, then it dies and the spikeball is rested
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter.player.playerDied();
            startSwing();
        }
    }    
    //Makes the object start swinging (not the best code for it but i works for the moment)
    private void startSwing()
    {
        rb.velocity = Vector2.one * 15;
    }
}
