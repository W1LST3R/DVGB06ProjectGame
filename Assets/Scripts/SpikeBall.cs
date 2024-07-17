using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpikeBall : MonoBehaviour
{
    public float swingDuration = 3f;  // Tid det tar att swinga till ena sidan
    public float swingAngle = 90f;    // Vinkel att swinga till (i grader)
    public Transform pivotPoint;      // Pivotpunkt för svängningen
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(swing());
    }

    IEnumerator swing()
    {
        while (true)
        {
            yield return swingToAngle(swingAngle);
            yield return swingToAngle(swingAngle*-1);
        }
    }

    IEnumerator swingToAngle(float angle)
    {
        Debug.Log(angle);
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle)) * startRotation;
        float elapsedTime = 0f;

        while (elapsedTime < swingDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / swingDuration);
            transform.RotateAround(pivotPoint.position, Vector3.forward, (angle / swingDuration) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Se till att vi når exakt slutvinkeln
        //transform.rotation = targetRotation;

        angle *= -1;
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(new Vector3(0, 0, angle)) * startRotation;
        elapsedTime = 0f;

        while (elapsedTime < swingDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / swingDuration);
            transform.RotateAround(pivotPoint.position, Vector3.forward, (angle / swingDuration) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Se till att vi når exakt slutvinkeln
        //transform.rotation = targetRotation;
    }
}
