using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public static bool isDead = false;

    //if the player hits the head zone
    //Note could do this better but works for the moment
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDead = true;
        }
    }
}
