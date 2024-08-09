using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    public GameObject woodBullet;
    public Transform shootPosition;
    public float spawnRate = 8;
    private float timer = 0;
    private bool playerInSight = false;
    public Animator animator;
    public bool right = false;
    private AudioManager audioManager;
    private float shootPositionDiff;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    //Gets the height diffrens between body and shot positon
    private void Start()
    {
        shootPositionDiff = shootPosition.position.y - gameObject.transform.position.y;
    }
    void Update()
    {
        //Sets the shoot positon if the timer reaches the limit, then it will shoot
        shootPosition.position = new Vector3(shootPosition.position.x, gameObject.transform.position.y + shootPositionDiff, shootPosition.position.z);
        if (playerInSight)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                audioManager.playSFX(audioManager.woodShoot);
                shootWoodBullet();
            }
        }
        

    }

    //spawns a wood bullet
    private void shootWoodBullet()
    {
        
        GameObject bullet = Instantiate(woodBullet, shootPosition.position, shootPosition.rotation);
        if (right) bullet.GetComponent<WoodBullet>().rightShoot(); 
    }

    //If player enters radius
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
            animator.SetBool("IsShooting", true);
        }
    }   
    //if player ¨stays in radius

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
            animator.SetBool("IsShooting", true);
        }
    }
    //if player leaves radius

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = false;
            animator.SetBool("IsShooting",false);
        }
    }

    //Sets virabels to zero and false
    public void playerLeftZone()
    {
        playerInSight = false;
        timer = 0;
    }

    //Kills enemy
    public void die()
    {
        StartCoroutine(playDeath());
    }

    //plays ab´nimation and destroys object
    IEnumerator playDeath()
    {
        audioManager.playSFX(audioManager.enemyDeath);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
