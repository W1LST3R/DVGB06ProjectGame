using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBullet : MonoBehaviour
{
    public float moveSpeed = 1;
    public float targetTime = 2;
    private float elapsedTime = 0;
    public bool leftShoot = true;

    // Update is called once per frame
    void Update()
    {
        //Moves the bullet until it reaches the limit
        if (leftShoot) transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        else transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > targetTime)
        {
            Destroy(gameObject);
        }
    }
    //Switch direction
    public void rightShoot()
    {
        leftShoot = false;
    }
}
