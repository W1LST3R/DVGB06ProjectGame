using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
  public static bool touchingWall;

    private void OnCollisionStay2D(Collision2D collision)
    {
        touchingWall = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touchingWall=false;
    }
}
