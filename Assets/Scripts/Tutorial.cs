using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private GameObject playerBody;
    private Text movementText;
    private Text dubbelJumpText;
    private Text wallJumpText;
    private Text angryPigText;
    private Text trunkText;
    private Text trapText;
    private Text endText;
    private void Start()
    {
        //Gets all the text objects
        playerBody = GameObject.FindGameObjectWithTag("Player");
        movementText = GameObject.FindGameObjectWithTag("MovementText").GetComponentInChildren<Text>();
        dubbelJumpText = GameObject.FindGameObjectWithTag("DubbelJumpText").GetComponentInChildren<Text>();
        wallJumpText = GameObject.FindGameObjectWithTag("WallJumpText").GetComponentInChildren<Text>();
        angryPigText = GameObject.FindGameObjectWithTag("AngryPigText").GetComponentInChildren<Text>();
        trunkText = GameObject.FindGameObjectWithTag("TrunkText").GetComponentInChildren<Text>();
        trapText = GameObject.FindGameObjectWithTag("TrapText").GetComponentInChildren<Text>();
        endText = GameObject.FindGameObjectWithTag("EndText").GetComponentInChildren<Text>();
    }
    void Update()
    {
        //Changes the tutorial text dependent where the player is
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
        else if (playerBody.transform.position.x > 73.05){
            trapText.enabled = false;
            endText.enabled = true;
        }

    }
}
