using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
 
    void Update()
    {
        if (target == null){
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if(SceneManager.GetActiveScene().name.Equals("StartScene")){
            transform.position = new Vector3(-84.36f, 0, -10);
        }else {
            //folows the players x movement but the camera stays on the same y position
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10); 
        }
        
    }
}
