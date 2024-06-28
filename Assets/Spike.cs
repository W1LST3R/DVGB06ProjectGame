using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player hits the spikes it dies
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter.player.playerDied();
        }
    }
}
