using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator animator;

    //If the player makes the checkpoint
    public void checkPointMade()
    {
        StartCoroutine(playAnimation());
    }


    //Plays animation for checkpoint
    IEnumerator playAnimation()
    {
        animator.SetTrigger("CheckPointHit");
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("CheckPointMade");
    }
}
