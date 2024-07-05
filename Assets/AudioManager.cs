using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("--------Audio Source--------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXEfects;

    [Header("--------Audio Clip--------")]
    public AudioClip background;
    public AudioClip enemyDeath;
    public AudioClip death;
    public AudioClip UI;
    public AudioClip wallJump;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play ();
    }
    public void playSFX(AudioClip clip)
    {
       SFXEfects.PlayOneShot(clip);
    }

}
