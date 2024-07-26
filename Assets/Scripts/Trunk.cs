using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
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
    private void shootWoodBullet()
    {
        
        GameObject bullet = Instantiate(woodBullet, shootPosition.position, shootPosition.rotation);
        if (right) bullet.GetComponent<WoodBullet>().rightShoot(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
            animator.SetBool("IsShooting", true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
            animator.SetBool("IsShooting", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = false;
            animator.SetBool("IsShooting",false);
        }
    }

    public void playerLeftZone()
    {
        playerInSight = false;
    }

    public void die()
    {
        StartCoroutine(playDeath());
    }

    IEnumerator playDeath()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        audioManager.playSFX(audioManager.enemyDeath);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
