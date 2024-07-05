using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
  public static bool touchingWall;
    //Sets true if the player is touching the wall
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingWall = true;
        }
    }
    //Sets false if the player is leaving the wall

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingWall = false;
        }
    }
}
