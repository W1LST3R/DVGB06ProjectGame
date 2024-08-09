using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
 
    void Update()
    {
        if (target == null){
            target = GameObject.FindGameObjectWithTag("Player");
        }
        //folows the players x movement but the camera stays on the same y position
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }
}
