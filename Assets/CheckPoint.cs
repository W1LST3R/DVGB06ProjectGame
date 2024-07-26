using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator animator;

    public void checkPointMade()
    {
        StartCoroutine(playAnimation());
    }

    IEnumerator playAnimation()
    {
        animator.SetTrigger("CheckPointHit");
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("CheckPointMade");
    }
}
