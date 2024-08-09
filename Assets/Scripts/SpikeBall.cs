using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpikeBall : MonoBehaviour
{
    public float swingDuration = 3f;  
    public float swingAngle = 90f;  
    public Transform pivotPoint;      
    private Vector3 startPosition;

    //Sets the ball in motion
    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(swing());
    }

    //Switches side it swings to
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
        //It swings from middle to left or right, then back to middle then it switch side
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

    }
}
