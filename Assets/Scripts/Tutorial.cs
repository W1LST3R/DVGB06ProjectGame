using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject playerBody;
    private Canvas movementText;
    private Canvas dubbelJumpText;
    private Canvas wallJumpText;
    private Canvas angryPigText;
    private Canvas trunkText;
    private Canvas trapText;
    private Canvas endText;
    private void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player");
        movementText = GameObject.FindGameObjectWithTag("MovementText").GetComponentInChildren<Canvas>();
        dubbelJumpText = GameObject.FindGameObjectWithTag("DubbelJumpText").GetComponentInChildren<Canvas>();
        wallJumpText = GameObject.FindGameObjectWithTag("WallJumpText").GetComponentInChildren<Canvas>();
        angryPigText = GameObject.FindGameObjectWithTag("AngryPigText").GetComponentInChildren<Canvas>();
        trunkText = GameObject.FindGameObjectWithTag("TrunkText").GetComponentInChildren<Canvas>();
        trapText = GameObject.FindGameObjectWithTag("TrapText").GetComponentInChildren<Canvas>();
        endText = GameObject.FindGameObjectWithTag("EndText").GetComponentInChildren<Canvas>();
    }
    void Update()
    {
        if (playerBody.transform.position.x < -2.65)
        {
            movementText.enabled = true;
            dubbelJumpText.enabled = false;
            wallJumpText.enabled = false;
            angryPigText.enabled = false;
            trunkText.enabled = false;
            trapText.enabled = false;
            endText.enabled = false;
        }
        else if (playerBody.transform.position.x < 4.98)
        {
            movementText.enabled = true;
            dubbelJumpText.enabled = false;
        }
        else if (playerBody.transform.position.x > 4.98 && playerBody.transform.position.x < 16.06)
        {
            movementText.enabled = false;
            dubbelJumpText.enabled = true;
            wallJumpText.enabled = false;
        }
        else if (playerBody.transform.position.x > 16.06 && playerBody.transform.position.x < 26.7)
        {
            dubbelJumpText.enabled = false;
            wallJumpText.enabled = true;
            angryPigText.enabled = false;
        }
        else if (playerBody.transform.position.x > 26.7 && playerBody.transform.position.x < 39.53)
        {
            wallJumpText.enabled = false;
            angryPigText.enabled = true;
            trunkText.enabled = false;
        }
        else if (playerBody.transform.position.x > 39.53 && playerBody.transform.position.x < 50.53)
        {
            angryPigText.enabled = false;
            trunkText.enabled = true;
            trapText.enabled = false;
        }
        else if (playerBody.transform.position.x > 50.53 && playerBody.transform.position.x < 73.05)
        {
            trunkText.enabled = false;
            trapText.enabled = true;
            endText.enabled = false;
        }

    }
}
